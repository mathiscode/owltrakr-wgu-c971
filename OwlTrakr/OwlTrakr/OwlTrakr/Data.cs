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

        //public static ObservableCollection<Term> Terms { get; set; }
        //public static ObservableCollection<Course> Courses = null; //new ObservableCollection<Course>();
        //public static ObservableCollection<Assessment> Assessments = null; //new ObservableCollection<Assessment>();

        public static void connect()
        {
            db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "OwlTrakr.db"));
            db.ExecuteAsync("DELETE FROM Term");
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
            return new ObservableCollection<Term>(list);

            //return Terms;
        }

        public static async Task<Term> FetchTerm(int termId)
        {
            return await db.FindAsync<Term>(termId);
        }

        async public static Task<Term> NewTerm(Term term)
        {
            await db.InsertAsync(term);
            //Terms.Add(term);
            return term;
        }

        public static Term UpdateTerm(Term term)
        {
            db.UpdateAsync(term);
            _ = FetchTerms();
            return term;
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
