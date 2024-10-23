import { DecimalPipe } from "@angular/common";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class CurrencyChange{

    inrToUsd(priceinInr: number) : number{
        var usdPrice =  priceinInr*(0.012);
        console.log(usdPrice);
        return usdPrice;
    }

}