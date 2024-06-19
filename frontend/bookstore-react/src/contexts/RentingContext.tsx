import { createContext, useContext, useState, ReactNode } from 'react';
import { listRentingsByClientId, registerRenting, returnRentingResource } from '../api/rentingApiRequest';
import { Renting } from '../interfaces/Renting';
import { ClientRenting } from '../interfaces/Client-renting';
import { RentingContextType } from '../interfaces/ContextType/RentingContextType';


const RentingContext = createContext<RentingContextType>({
  clientRentings: [],
  rentings: [],
  listRentingsByClientId: async () => [],
  registerRenting: async () => Promise.resolve({} as Renting),
  returnRentingResource: async () => Promise.resolve(),
});

export const useRentingContext = () => {
  return useContext(RentingContext);
}

const RentingContextProvider = ({ children }: { children: ReactNode }) => {
  const [clientRentings, setClientRentings] = useState<ClientRenting[]>([]);
  const [rentings, setRentings] = useState<Renting[]>([]);

  const fetchRentingsByClientId = async (id: string) => {
    try {
      const fetchedRentings = await listRentingsByClientId(id);
      setClientRentings(fetchedRentings);
      return fetchedRentings;
    } catch (error) {
      console.error('Error fetching client rentings:', error);
      throw error;
    }
  };

  const addNewRenting = async (data: Omit<Renting, 'id'>) => {
    try {
      const newRenting = await registerRenting(data);
      setRentings((prevRentings) => [...prevRentings, newRenting]);
      return newRenting;
    } catch (error) {
      console.error('Error registering renting:', error);
      throw error;
    }
  };

  const returnResource = async (id: string, returnDate: Date | string) => {
    try {
      await returnRentingResource(id, returnDate);
      setRentings((prevRentings) =>
        prevRentings.map((renting) => (renting.id === id ? { ...renting, returnDate } : renting))
      );
    } catch (error) {
      console.error('Error returning renting resource:', error);
      throw error;
    }
  };

  return (
    <RentingContext.Provider value={{
      clientRentings,
      rentings,
      listRentingsByClientId: fetchRentingsByClientId,
      registerRenting: addNewRenting,
      returnRentingResource: returnResource,
    }}>
      {children}
    </RentingContext.Provider>
  );
}

export default RentingContextProvider;