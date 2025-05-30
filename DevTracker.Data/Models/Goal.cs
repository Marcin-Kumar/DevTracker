﻿using DevTracker.Core.Domain.Entities.Enums;

namespace DevTracker.Data.Models;

public class Goal
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public DateTime AchieveBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public TimeSpan DailyTargetHours { get; set; }
    public List<Project> Projects { get; set; } = [];
    public Status CurrentStatus { get; set; }
    public List<Session> CodingSessions { get; set; } = [];
    public List<Session> TheorySessions { get; set; } = [];
}