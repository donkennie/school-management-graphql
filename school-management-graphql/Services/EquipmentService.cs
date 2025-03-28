﻿using Microsoft.EntityFrameworkCore;
using school_management_graphql.Data;
using school_management_graphql.Models;

namespace school_management_graphql.Services
{
    public class EquipmentService(IDbContextFactory<AppDbContext> contextFactory) : IEquipmentService
    {
        public async Task<List<Equipment>> GetEquipmentListAsync()
        {
            await using var dbContext = await contextFactory.CreateDbContextAsync();
            var equipment = await dbContext.Equipment.ToListAsync();
            return equipment;
        }

        public async Task<Equipment> GetEquipmentAsync(Guid equipmentId)
        {
            await using var dbContext = await contextFactory.CreateDbContextAsync();
            var equipment = await dbContext.Equipment.FindAsync(equipmentId);
            return equipment ?? throw new ArgumentException("Equipment not found", nameof(equipmentId));
        }
    }
}
