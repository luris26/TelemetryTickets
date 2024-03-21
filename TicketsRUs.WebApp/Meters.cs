using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.SignalR;

public static class Meters{
    public static int LurisCount {get;set;}

    public static string myServiceName = "LurisMetricsName";
    public static Meter myhomeworkmeter = new Meter("LurisHomework.DemoTicket", "2.0.7");
    public static Counter<int> quantity = myhomeworkmeter.CreateCounter<int>("counter_ticket_Luris", description: "This shoul be counting something");
}