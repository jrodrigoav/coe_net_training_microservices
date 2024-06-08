import apiConfig from '../assets/apiConfig.json';
import { Inventory } from '../interfaces/Inventory';
import { InventorySummary } from '../interfaces/Inventory-summary';

// Fetch the list of inventory summaries
export const getInventorySummary = async (): Promise<InventorySummary[]> => {
    try {
        const response = await fetch(apiConfig.api.inventoryApi + '/summary');
        if (response.ok) {
            return await response.json();
        } else {
            console.error("Error fetching inventory summary:", response.statusText);
            throw new Error("Error fetching inventory summary");
        }
    } catch (error) {
        console.error("Error trying to fetch inventory summary:", error);
        throw new Error("Error trying to fetch inventory summary");
    }
};

// Register a new inventory resource
export const registerInventoryResource = async (resourceId: string): Promise<Inventory> => {
    try {
        const response = await fetch(apiConfig.api.inventoryApi + '/register', {
            method: 'POST',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ resourceId })
        });

        if (response.ok) {
            return await response.json();
        } else {
            console.error("Error registering inventory resource:", response.statusText);
            throw new Error("Error registering inventory resource");
        }
    } catch (error) {
        console.error("Error trying to register inventory resource:", error);
        throw new Error("Error trying to register inventory resource");
    }
};
