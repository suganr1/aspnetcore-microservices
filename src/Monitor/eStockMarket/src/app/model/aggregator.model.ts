import { Company } from "./company.model"
import { Stock } from "./stock.model";

export class Aggregator {
    companyCode!: string | null;
    companyDetails!: Company;
    stockDetails!: Stock[]
}
