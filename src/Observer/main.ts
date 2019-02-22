//#region Data Model
class WeatherStationData {
    StationName: string;
    Temprature: number;
    Humidity: number;
    Percipitation: number;
    Date: Date;

    constructor(stationName: string, temprature: number, humidity: number, percipitation: number) {
        this.StationName = stationName;
        this.Temprature = temprature;
        this.Humidity = humidity;
        this.Percipitation = percipitation;
        this.Date = new Date();
    }
}
//#endregion

//#region Observer

interface IObserver {
    Act(data: WeatherStationData): void;
}


class TempratureObserver implements IObserver {
    Act(data: WeatherStationData): void {
        console.log(`${data.Date.toISOString()} - ${data.StationName}> \t temprature is ${data.Temprature} degree`);
    }
}

class HumidityObserver implements IObserver {
    Act(data: WeatherStationData): void {
        console.log(`${data.Date.toISOString()} - ${data.StationName}> \t humidity is ${data.Humidity}%`);
    }
}

class PercipitationObserver implements IObserver {
    Act(data: WeatherStationData): void {
        console.log(`${data.Date.toISOString()} - ${data.StationName}> \t percipitation is ${data.Percipitation} mm`);
    }
}
//#endregion

//#region Observable (Subject)
interface IObservable {
    Subscribe(subscriber: IObserver): void;
    NotifySubscribers(): void;
}

class WeatherStation implements IObservable {

    private subscribers: Array<IObserver> = new Array<IObserver>();

    private StationName: string;

    constructor(stationName: string) {

        this.StationName = stationName;
    }

    public Subscribe(subscriber: IObserver): void {
        this.subscribers.push(subscriber);
    }
    NotifySubscribers(): void {

        let mockData = new WeatherStationData(
            this.StationName,
            Math.random() * 7,
            Math.random() * 11,
            Math.random() * 10);

        this.subscribers.forEach(s => s.Act(mockData));
    }

    public StartWorking() {
        setInterval(() => {
            console.log(`Station ${this.StationName} reporting it's data`);
            this.NotifySubscribers();
        }, 1000)
    }
}
//#endregion

class WeatherReporter {
    public static GatherReports() {
        let s1 = new WeatherStation("Tehran");
        s1.Subscribe(new TempratureObserver());
        s1.Subscribe(new HumidityObserver());

        let s2 = new WeatherStation("KhoramAbad");
        s2.Subscribe(new PercipitationObserver());

        s1.StartWorking();
        s2.StartWorking();
    }
}

WeatherReporter.GatherReports();