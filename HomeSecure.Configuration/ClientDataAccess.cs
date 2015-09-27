using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using HomeSecure.Data.Entities;
using HomeSecure.Data.Interfaces;

namespace HomeSecure.Client.Configuration
{
    public class ClientDataAccess: DbContext
    {
        private IList<IDataBaseExtender> _dbExtenders;

        public ClientDataAccess(string connectionString, IList<IDataBaseExtender> dbExtenders) :
            base(connectionString)
        {
            _dbExtenders = dbExtenders;
            if (_dbExtenders == null)
            {
                _dbExtenders = new List<IDataBaseExtender>();
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            /*http://blogs.microsoft.co.il/blogs/gilf/archive/2011/04/17/spreading-inheritance-tree-mapping-across-assemblies-in-code-first.aspx
             * */
            
            foreach (IDataBaseExtender dbExtender in _dbExtenders)
            {
                dbExtender.OnModelCreating(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity
        }

        //public DbSet<LocalCameraDevice> LocalCameras { get; set; }
        public DbSet<CameraDevice> CameraDevices { get; set; }
        public DbSet<MotionDetectionEvent> MotionDetectionEvents { get; set; }
        public DbSet<NotificationEntityData> NotificationEntities { get; set; }
        public DbSet<NotificationEntityParams> NotificationEntityParameters { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }
    }
}
