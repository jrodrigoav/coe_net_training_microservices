import apiConfig from '../assets/apiConfig.json';
import { Client } from '../interfaces/Client';

// Fetch the list of clients
export const getClients = async (): Promise<Client[]> => {
    try {
        const response = await fetch(apiConfig.api.clientApi);
        if (response.ok) {
            return await response.json();
        } else {
            console.error("Error fetching clients:", response.statusText);
            throw new Error("Error fetching clients");
        }
    } catch (error) {
        console.error("Error trying to fetch clients:", error);
        throw new Error("Error trying to fetch clients");
    }
};

// Create a new client
export const createClient = async (client: Omit<Client, 'id'>): Promise<Client> => {
    try {
        const response = await fetch(apiConfig.api.clientApi, {
            method: 'POST',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(client)
        });

        if (response.ok) {
            return await response.json();
        } else {
            console.error("Error creating client:", response.statusText);
            throw new Error("Error creating client");
        }
    } catch (error) {
        console.error("Error trying to create client:", error);
        throw new Error("Error trying to create client");
    }
};

// Update an existing client
export const updateClient = async (id: string, client: Omit<Client, 'id'>): Promise<Client> => {
    try {
        const response = await fetch(`${apiConfig.api.clientApi}/${id}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(client)
        });

        if (response.ok) {
            return await response.json();
        } else {
            console.error("Error updating client:", response.statusText);
            throw new Error("Error updating client");
        }
    } catch (error) {
        console.error("Error trying to update client:", error);
        throw new Error("Error trying to update client");
    }
};

// Delete a client
export const deleteClient = async (id: string): Promise<void> => {
    try {
        const response = await fetch(`${apiConfig.api.clientApi}/${id}`, {
            method: 'DELETE',
        });

        if (!response.ok) {
            console.error("Error deleting client:", response.statusText);
            throw new Error("Error deleting client");
        }
    } catch (error) {
        console.error("Error trying to delete client:", error);
        throw new Error("Error trying to delete client");
    }
};