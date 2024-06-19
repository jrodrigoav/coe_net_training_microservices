import { Client } from "../Client";

export interface ClientContextType {
    clients: Client[];
    getClients: () => Promise<void>;
    createClient: (client: Omit<Client, 'id'>) => Promise<Client>;
    updateClient: (id: string, client: Omit<Client, 'id'>) => Promise<Client>;
    deleteClient: (id: string) => Promise<void>;
}