using OwlTrakr.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace OwlTrakr
{
    static class Data
    {
        private static SQLiteAsyncConnection db;
        public static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "OwlTrakr.db");

        async public static Task Connect()
        {
            db = new SQLiteAsyncConnection(dbPath);
            SQLite.CreateTablesResult result = await db.CreateTablesAsync<Term, Course, Assessment>();

            bool hasEntries = (await CountTerms()) > 0;
            if (!hasEntries) await CreateSampleRecords();
        }

        public static async Task CreateSampleRecords()
        {
            DateTime start = DateTime.Today;

            Term term = new Term()
            {
                Title = "Sample Term 1",
                Start = start,
                End = start.AddMonths(1),
                NotificationsEnabled = true
            };

            await db.InsertAsync(term);

            Course course = new Course()
            {
                Title = "Sample Course A",
                Status = "Plan to Take",
                Start = start,
                End = start.AddDays(14),
                InstructorName = "Jamey Mathis",
                InstructorPhone = "870-201-5606",
                InstructorEmail = "jmat159@wgu.edu",
                Notes = "This is a sample course.",
                NotificationsEnabled = true,
                TermId = term.Id
            };

            await db.InsertAsync(course);

            Assessment performanceAssessment = new Assessment()
            {
                Title = "Sample Assessment I",
                Type = "Performance Assessment",
                Start = start,
                End = start.AddDays(3),
                NotificationsEnabled = true,
                TermId = term.Id,
                CourseId = course.Id
            };

            Assessment objectiveAssessment = new Assessment()
            {
                Title = "Sample Assessment II",
                Type = "Objective Assessment",
                Start = start,
                End = start.AddDays(7),
                NotificationsEnabled = true,
                TermId = term.Id,
                CourseId = course.Id
            };

            await db.InsertAsync(performanceAssessment);
            await db.InsertAsync(objectiveAssessment);
        }

        public static async Task<int> CountTerms()
        {
            return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Term");
        }

        public static async Task<int> CountCourses(Term term)
        {
            return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Course WHERE TermId = " + term.Id);
        }

        public static async Task<int> CountAssessments(Course course)
        {
            return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Assessment WHERE CourseId = " + course.Id);
        }


        public static async Task<ObservableCollection<Term>> FetchTerms()
        {
            AsyncTableQuery<Term> query = db.Table<Term>();
            List<Term> list = await query.ToListAsync();
            return new ObservableCollection<Term>(list);
        }

        public static async Task<Term> FetchTerm(int termId)
        {
            return await db.FindAsync<Term>(termId);
        }

        async public static Task<Term> NewTerm(Term term)
        {
            await db.InsertAsync(term);
            return term;
        }

        async public static Task<Term> UpdateTerm(Term term)
        {
            await db.UpdateAsync(term);
            return term;
        }

        async public static Task<Term> DeleteTerm(Term term)
        {
            AsyncTableQuery<Assessment> assessmentQuery = db.Table<Assessment>().Where(a => a.TermId == term.Id);
            await assessmentQuery.DeleteAsync();
            AsyncTableQuery<Course> courseQuery = db.Table<Course>().Where(c => c.TermId == term.Id);
            await courseQuery.DeleteAsync();
            await db.DeleteAsync(term);
            return term;
        }

        public static async Task<ObservableCollection<Course>> FetchCourses()
        {
            AsyncTableQuery<Course> query = db.Table<Course>();
            List<Course> list = await query.ToListAsync();
            return new ObservableCollection<Course>(list);
        }

        public static async Task<ObservableCollection<Course>> FetchCourses(int termId)
        {
            AsyncTableQuery<Course> query = db.Table<Course>().Where(c => c.TermId == termId);
            List<Course> list = await query.ToListAsync();
            return new ObservableCollection<Course>(list);
        }

        async public static Task<Course> NewCourse(Course course)
        {
            await db.InsertAsync(course);
            return course;
        }

        async public static Task<Course> UpdateCourse(Course course)
        {
            await db.UpdateAsync(course);
            return course;
        }

        async public static Task<Course> DeleteCourse(Course course)
        {
            AsyncTableQuery<Assessment> assessmentQuery = db.Table<Assessment>().Where(a => a.CourseId == course.Id);
            await assessmentQuery.DeleteAsync();
            await db.DeleteAsync(course);
            return course;
        }

        public static async Task<ObservableCollection<Assessment>> FetchAssessments()
        {
            AsyncTableQuery<Assessment> query = db.Table<Assessment>();
            List<Assessment> list = await query.ToListAsync();
            return new ObservableCollection<Assessment>(list);
        }

        public static async Task<ObservableCollection<Assessment>> FetchAssessments(Term term, Course course)
        {
            AsyncTableQuery<Assessment> query = db.Table<Assessment>().Where(a => a.TermId == term.Id && a.CourseId == course.Id);
            List<Assessment> list = await query.ToListAsync();
            return new ObservableCollection<Assessment>(list);
        }

        async public static Task<Assessment> NewAssessment(Assessment assessment)
        {
            await db.InsertAsync(assessment);
            return assessment;
        }

        async public static Task<Assessment> UpdateAssessment(Assessment assessment)
        {
            await db.UpdateAsync(assessment);
            return assessment;
        }

        async public static Task<Assessment> DeleteAssessment(Assessment assessment)
        {
            await db.DeleteAsync(assessment);
            return assessment;
        }
    }
}
