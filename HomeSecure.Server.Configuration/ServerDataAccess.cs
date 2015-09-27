using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using HomeSecure.Data.Entities;
using HomeSecure.Log;
using System.Data;
using System.Data.SqlClient;

namespace HomeSecure.Server.Configuration
{
    public class ServerDataAccess : DbContext
    {
        private string _connectionString;

        public DbSet<MotionDetectionEvent> MotionDetectionEvents { get; set; }
        public DbSet<CameraDevice> CameraDevices { get; set; }


        public ServerDataAccess(string connectionString) :
            base(connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddMotionDetectionEvent(MotionDetectionEvent motionDetectionEvent)
        {
            try
            {
                //execute stored procedure with parameter
                //CameraDevice cd = Database.SqlQuery<CameraDevice>("sp_GetCameraDeviceByID @p0", motionDetectionEvent.CameraDevice.ID).FirstOrDefault();

                CameraDevice cd = CameraDevices.Where(a => a.ID == motionDetectionEvent.CameraDevice.ID).FirstOrDefault();

                if (cd == null)
                {
                    cd = CameraDevices.Add(motionDetectionEvent.CameraDevice);
                }
                else
                {
                    motionDetectionEvent.CameraDevice = cd;
                }
                MotionDetectionEvents.Add(motionDetectionEvent);
                SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to add motion detection event", ex);
            }
        }

        public List<MotionDetectionEvent> GetMotionDetectionEvents(int pageSize, int pageNumber, out int total)
        {

            //sample of executing stored procedure with "OUT" parameter

            List<MotionDetectionEvent> result = null;
            total = 0;

            try
            {
                var outParam = new SqlParameter();
                outParam.ParameterName = "@Total";
                outParam.SqlDbType = SqlDbType.Int;
                outParam.Direction = ParameterDirection.Output;

                var motionDetectionEvents = Database.SqlQuery<MotionDetectionEvent>("sp_GetMotionDetectionEvents @PageSize, @PageNumber, @Total OUTPUT",
                    new SqlParameter("@PageSize",pageSize),
                    new SqlParameter("@PageNumber",pageNumber),
                    outParam);

                result = motionDetectionEvents.ToList();

                total = (int)outParam.Value;

                
            }
            catch (Exception ex)
            {
                result = new List<MotionDetectionEvent>();
                Logger.Error("Failed to get motion detection events", ex);
            }

            return result;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //map the CameraDevice property to FK column "CameraID"
            modelBuilder.Entity<MotionDetectionEvent>()
                .HasRequired(a => a.CameraDevice)
                .WithMany()
                .Map(m => m.MapKey("CameraID"));

            base.OnModelCreating(modelBuilder);

        }
    }
}
