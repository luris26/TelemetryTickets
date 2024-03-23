using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.SignalR;
using TicketsRUs.WebApp.Components.Pages;

public static class Meters{

    public static string myServiceName = "LurisMetricsName";

    public static Meter myhomeworkmeter = new Meter(myServiceName, "2.0.7");
    public static Counter<int> quantity = myhomeworkmeter.CreateCounter<int>("counterLuris");

    public static string histogramService = "Histogram";

    public static Meter histogram = new Meter(histogramService, "2.0.7");


    //public static Meter histo = new Meter(myServiceName, "2.0.7");
    public static Histogram<double> histogram_luris = histogram.CreateHistogram<double>("Histogram_Luris");




    //public static Meter gauge = new Meter(myServiceName, "2.0.7");

    public static UpDownCounter<int> UpDownLuris = myhomeworkmeter.CreateUpDownCounter<int>("UpdownCOunterLuris");


    public static ObservableGauge<int> observaGauge_luris = myhomeworkmeter.CreateObservableGauge<int>("gauge_luris", () => Home.gauge);


    public static ObservableUpDownCounter<int> observable_up_down_luris = myhomeworkmeter.CreateObservableUpDownCounter<int>("observableUpDownLuris", () => Home.observabledown);


    public static ObservableCounter<int> observable_counter = myhomeworkmeter.CreateObservableCounter<int>("observablecounterLuris", () => Home.currentCount);

    
    
}