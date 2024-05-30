import { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { getInventorySummary, registerInventoryResource } from '../api/inventoryApiRequest';
import { InventoryContextType } from '../interfaces/ContextType/InventoryContextType';
import { Inventory } from '../interfaces/Inventory';
import { InventorySummary } from '../interfaces/Inventory-summary';


const InventoryContext = createContext<InventoryContextType>({
  inventorySummary: [],
  getInventorySummary: async () => {},
  registerInventoryResource: async () => Promise.resolve({} as Inventory),
});

export const useInventoryContext = () => {
  return useContext(InventoryContext);
}

const InventoryContextProvider = ({ children }: { children: ReactNode }) => {
  const [inventorySummary, setInventorySummary] = useState<InventorySummary[]>([]);

  const fetchInventorySummary = async () => {
    try {
      const fetchedInventorySummary = await getInventorySummary();
      setInventorySummary(fetchedInventorySummary);
    } catch (error) {
      console.error('Error fetching inventory summary:', error);
    }
  };

  useEffect(() => {
    fetchInventorySummary();
  }, []);

  const addNewInventoryResource = async (resourceId: string) => {
    try {
      const newResource = await registerInventoryResource(resourceId);
      fetchInventorySummary();
      return newResource;
    } catch (error) {
      console.error('Error registering inventory resource:', error);
      throw error;
    }
  };

  return (
    <InventoryContext.Provider value={{
      inventorySummary,
      getInventorySummary: fetchInventorySummary,
      registerInventoryResource: addNewInventoryResource,
    }}>
      {children}
    </InventoryContext.Provider>
  );
}

export default InventoryContextProvider;
