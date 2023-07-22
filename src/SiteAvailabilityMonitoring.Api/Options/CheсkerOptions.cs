using System;

namespace SiteAvailabilityMonitoring.Api.Options;

public class CheckerOptions
{
    public bool IsEnabled { get; set; }
    public TimeSpan DelayTimeSpan { get; set; }
    public TimeSpan ErrorDelayTimeSpan { get; set; }
}