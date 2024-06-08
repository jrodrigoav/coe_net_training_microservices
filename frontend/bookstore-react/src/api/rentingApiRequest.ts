import apiConfig from '../assets/apiConfig.json';
import { ClientRenting } from '../interfaces/Client-renting';
import { Renting } from '../interfaces/Renting';


// Fetch the list of renting records by client ID
export const listRentingsByClientId = async (id: string): Promise<ClientRenting[]> => {
    try {
        const response = await fetch(`${apiConfig.api.rentingApi}/client/${id}`);
        if (response.ok) {
            return await response.json();
        } else {
            console.error("Error fetching rentings:", response.statusText);
            throw new Error("Error fetching rentings");
        }
    } catch (error) {
        console.error("Error trying to fetch rentings:", error);
        throw new Error("Error trying to fetch rentings");
    }
};

// Register a new renting record
export const registerRenting = async (data: Omit<Renting, 'id'>): Promise<Renting> => {
    try {
      const response = await fetch(`${apiConfig.api.rentingApi}/register`, {
        method: 'POST',
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data)
      });
  
      if (response.ok) {
        return await response.json();
      } else {
        console.error("Error registering renting:", response.statusText);
        throw new Error("Error registering renting");
      }
    } catch (error) {
      console.error("Error trying to register renting:", error);
      throw new Error("Error trying to register renting");
    }
  };

// Return a renting resource
export const returnRentingResource = async (id: string, returnDate: Date | string): Promise<void> => {
    try {
        const response = await fetch(`${apiConfig.api.rentingApi}/return/${id}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ returnDate })
        });

        if (!response.ok) {
            console.error("Error returning renting resource:", response.statusText);
            throw new Error("Error returning renting resource");
        }
    } catch (error) {
        console.error("Error trying to return renting resource:", error);
        throw new Error("Error trying to return renting resource");
    }
};
