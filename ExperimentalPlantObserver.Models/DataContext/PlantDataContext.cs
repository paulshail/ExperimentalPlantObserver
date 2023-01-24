using ExperimentalPlantObserver.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.DataContext
{
    public class PlantDataContext : DbContext
    {
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

        public PlantDataContext(DbContextOptions<PlantDataContext> options) : base(options){}
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // TODO add foreign keys
        }


        }
}
