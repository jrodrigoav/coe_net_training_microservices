import { Inventory } from "../Inventory";
import { InventorySummary } from "../Inventory-summary";

export interface InventoryContextType {
  inventorySummary: InventorySummary[];
  getInventorySummary: () => Promise<void>;
  registerInventoryResource: (resourceId: string) => Promise<Inventory>;
}