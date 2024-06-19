import { ClientRenting } from "../Client-renting";
import { Renting } from "../Renting";

export interface RentingContextType {
  clientRentings: ClientRenting[];
  rentings: Renting[];
  listRentingsByClientId: (id: string) => Promise<ClientRenting[]>;
  registerRenting: (data: Omit<Renting, 'id'>) => Promise<Renting>;
  returnRentingResource: (id: string, returnDate: Date | string) => Promise<void>;
}