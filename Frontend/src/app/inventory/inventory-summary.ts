export interface InventorySummary {
    resourceId: string;
    resourceName: string;
    availableCopies: number;
    unavailableCopies: number;
    totalCopies: number;
}