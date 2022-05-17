using OwlTrakr.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;

namespace OwlTrakr
{
    static class Data
    {
        private static SQLiteAsyncConnection db;

        public static ObservableCollection<Term> Terms = null; //new ObservableCollection<Term>();
        public static ObservableCollection<Course> Courses = null; //new ObservableCollection<Course>();
        public static ObservableCollection<Assessment> Assessments = null; //new ObservableCollection<Assessment>();

        public static Term currentTerm = null;
        public static Course currentCourse = null;
        public static Assessment currentAssessment = null;

        public static void connect()
        {
            db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "OwlTrakr.db"));
            // db.ExecuteAsync("DELETE FROM Term");
            db.CreateTablesAsync<Term, Course, Assessment>();
        }

        public static async Task<int> CountTerms()
        {
            return await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Term");
        }

        public static async Task<ObservableCollection<Term>> FetchTerms()
        {
            AsyncTableQuery<Term> query = db.Table<Term>();
            List<Term> list = await query.ToListAsync();
            Terms = new ObservableCollection<Term>(list);

            Data.Terms.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                if (Terms == null || Terms.Count == 0)
                {
                    TermList.termListEmptyLabel.IsVisible = true;
                    TermList.termListView.IsVisible = false;
                }
                else
                {
                    TermList.termListEmptyLabel.IsVisible = false;
                    TermList.termListView.IsVisible = true;
                }
            };

            return Terms;
        }

        public static async Task<Term> FetchTerm(int termId)
        {
            return await db.FindAsync<Term>(termId);
        }

        public static void NewTerm(Term term)
        {
            db.InsertAsync(term);
            Terms.Add(term);
        }

        public static void UpdateTerm(Term term)
        {
            db.UpdateAsync(term);
            _ = FetchTerms();
        }

        public static void DeleteTerm(Term term)
        {
            db.DeleteAsync(term);
            _ = FetchTerms();
        }

        public static async Task<ObservableCollection<Course>> FetchCourses(int termId)
        {
            AsyncTableQuery<Course> query = db.Table<Course>();
            List<Course> list = await query.ToListAsync();
            return new ObservableCollection<Course>(list);
        }
    }
}
