using ExperimentalPlantObserver.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.DataContext
{
    public class PlantDataContext : DbContext
    {

        private string _connString = "Data Source=RONLAPTOP;Initial Catalog=EPODatabase;Integrated Security=True";

        #region Tables

        //Entity framework creation of database
        public DbSet<Cluster> Clusters { get; init; }
        public DbSet<ClusterCrop> ClusterCrops { get; init; }
        public DbSet<ClusterData> ClusterDatas { get; init; }
        public DbSet<ClusterDataType> ClusterDataTypes { get; init; }
        public DbSet<ClusterLocation> ClusterLocations { get; init; }
        public DbSet<ClusterSoil> ClusterSoil { get; init; }
        public DbSet<MeasurementUnit> MeasurementUnits { get; init; }
        public DbSet<Sensor> Sensors { get; init; }
        public DbSet<SensorCluster> SensorClusters { get; init; }
        public DbSet<SensorMeasurement> SensorMeasurements { get; init; }
        public DbSet<SensorType> SensorTypes { get; init; }


        #endregion
        #region ctors

        public PlantDataContext(DbContextOptions<PlantDataContext> options) : base(options)
        {

        }

        public PlantDataContext()
        {

        }
        #endregion


        #region Config

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Foreign keys definitions


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        #endregion
    }

}
