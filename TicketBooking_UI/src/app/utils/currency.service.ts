import { DecimalPipe } from "@angular/common";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class CurrencyChange{

    inrToUsd(priceinInr: number | undefined) : number{
        if(priceinInr === undefined){
            return 0;
        }
        var usdPrice =  priceinInr*(0.012);
        console.log(usdPrice);
        return usdPrice;
    }

    calcTaxes(price: number | undefined) : number{
        if(price === undefined){
            return 0;
        }

        return 0.18*price;
    }

}