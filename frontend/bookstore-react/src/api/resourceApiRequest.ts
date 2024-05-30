import apiConfig from '../assets/apiConfig.json';
import { Resource } from '../interfaces/Resource';

// Fetch the list of resources
export const getResourceList = async (): Promise<Resource[]> => {
    try {
        const response = await fetch(apiConfig.api.resourceApi);
        if (response.ok) {
            return await response.json();
        } else {
            console.error("Error fetching resources:", response.statusText);
            throw new Error("Error fetching resources");
        }
    } catch (error) {
        console.error("Error trying to fetch resources:", error);
        throw new Error("Error trying to fetch resources");
    }
};

// Create a new resource
export const createResource = async (resource: Omit<Resource, 'id'>): Promise<Resource> => {
    try {
        const response = await fetch(apiConfig.api.resourceApi, {
            method: 'POST',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(resource)
        });

        if (response.ok) {
            return await response.json();
        } else {
            console.error("Error creating resource:", response.statusText);
            throw new Error("Error creating resource");
        }
    } catch (error) {
        console.error("Error trying to create resource:", error);
        throw new Error("Error trying to create resource");
    }
};

// Update an existing resource
export const updateResource = async (id: string, resource: Omit<Resource, 'id'>): Promise<Resource> => {
    try {
        const response = await fetch(`${apiConfig.api.resourceApi}/${id}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ id, ...resource })
        });

        if (response.ok) {
            return await response.json();
        } else {
            console.error("Error updating resource:", response.statusText);
            throw new Error("Error updating resource");
        }
    } catch (error) {
        console.error("Error trying to update resource:", error);
        throw new Error("Error trying to update resource");
    }
};

// Delete a resource
export const deleteResource = async (id: string): Promise<boolean> => {
    try {
        const response = await fetch(`${apiConfig.api.resourceApi}/${id}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            return true;
        } else {
            console.error("Error deleting resource:", response.statusText);
            throw new Error("Error deleting resource");
        }
    } catch (error) {
        console.error("Error trying to delete resource:", error);
        throw new Error("Error trying to delete resource");
    }
};