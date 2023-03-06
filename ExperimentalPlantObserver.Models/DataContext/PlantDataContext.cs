using ExperimentalPlantObserver.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.DataContext
{
    public class PlantDataContext : DbContext
    {



        #region Tables

        //Entity framework creation of database
        public DbSet<Cluster> Cluster { get; init; }
        public DbSet<ClusterCrop> ClusterCrop { get; init; }
        public DbSet<ClusterData> ClusterData { get; init; }
        public DbSet<ClusterDataType> ClusterDataType { get; init; }
        public DbSet<ClusterLocation> ClusterLocation { get; init; }
        public DbSet<ClusterSoil> ClusterSoil { get; init; }
        public DbSet<MeasurementUnit> MeasurementUnit { get; init; }
        public DbSet<Sensor> Sensor { get; init; }
        public DbSet<SensorCluster> SensorCluster { get; init; }
        public DbSet<SensorMeasurement> SensorMeasurement { get; init; }
        public DbSet<SensorType> SensorType { get; init; }


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
