import { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { getClients, createClient, updateClient, deleteClient } from '../api/clientApiRequest';
import { Client } from '../interfaces/Client';
import { ClientContextType } from '../interfaces/ContextType/ClientContextType';

const Context = createContext<ClientContextType>({
  clients: [],
  getClients: async () => {},
  createClient: async () => Promise.resolve({} as Client),
  updateClient: async () => Promise.resolve({} as Client),
  deleteClient: async () => Promise.resolve(),
});

export const useClientContext = () => {
  return useContext(Context);
}

const ClientContextProvider = ({ children }: { children: ReactNode }) => {
  const [clients, setClients] = useState<Client[]>([]);

  const fetchClients = async () => {
    try {
      const fetchedClients = await getClients();
      setClients(fetchedClients);
    } catch (error) {
      console.error('Error fetching clients:', error);
    }
  };

  useEffect(() => {
    fetchClients();
  }, []);

  const createNewClient = async (client: Omit<Client, 'id'>) => {
    try {
      const newClient = await createClient(client);
      setClients((prevClients) => [...prevClients, newClient]);
      return newClient;
    } catch (error) {
      console.error('Error creating a new client:', error);
      throw error;
    }
  };

  const updateExistingClient = async (id: string, client: Omit<Client, 'id'>) => {
    try {
      const updatedClient = await updateClient(id, client);
      setClients((prevClients) =>
        prevClients.map((c) => (c.id === id ? updatedClient : c))
      );
      return updatedClient;
    } catch (error) {
      console.error('Error updating the client:', error);
      throw error;
    }
  };

  const removeClient = async (id: string) => {
    try {
      await deleteClient(id);
      setClients((prevClients) => prevClients.filter((c) => c.id !== id));
    } catch (error) {
      console.error('Error deleting the client:', error);
      throw error;
    }
  };

  return (
    <Context.Provider value={{
      clients,
      getClients: fetchClients,
      createClient: createNewClient,
      updateClient: updateExistingClient,
      deleteClient: removeClient,
    }}>
      {children}
    </Context.Provider>
  );
}

export default ClientContextProvider;
