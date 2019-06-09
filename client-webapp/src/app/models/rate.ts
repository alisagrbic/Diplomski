export class Rate {
    ClientID: number;
    ServiceID: number;
    Value: number;

    constructor(serid: number, value: number, client: number){
        this.ServiceID=serid;
        this.Value = value;
        this.ClientID=client;
    }
}
