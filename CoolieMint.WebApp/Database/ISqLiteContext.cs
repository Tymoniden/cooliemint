using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using WebControlCenter.Database.Entities;

namespace WebControlCenter.Database
{
    public interface ISqLiteContext : IDisposable
    {
        EntityEntry Add(object entity);

        int SaveChanges();

        DbSet<Controller> Controller { get; set; }
        DbSet<ControllerStatusSegment> ControllerStatusSegment { get; set; }
        DbSet<ControllerStateInformation> ControllerStateInformation { get; set; }
        DbSet<ControllerStateHistory> ControllerStateHistory { get; set; }
        DbSet<VersionHistory> VersionHistory { get; set; }
    }
}