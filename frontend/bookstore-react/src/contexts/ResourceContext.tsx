import { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { getResourceList, createResource, updateResource, deleteResource } from '../api/resourceApiRequest';
import { Resource } from '../interfaces/Resource';
import { ResourceContextType } from '../interfaces/ContextType/ResourceContextType';


const ResourceContext = createContext<ResourceContextType>({
  resources: [],
  getResourceList: async () => {},
  createResource: async () => Promise.resolve({} as Resource),
  updateResource: async () => Promise.resolve({} as Resource),
  deleteResource: async () => Promise.resolve(false),
});

export const useResourceContext = () => {
  return useContext(ResourceContext);
}

const ResourceContextProvider = ({ children }: { children: ReactNode }) => {
  const [resources, setResources] = useState<Resource[]>([]);

  const fetchResourceList = async () => {
    try {
      const fetchedResources = await getResourceList();
      setResources(fetchedResources);
    } catch (error) {
      console.error('Error fetching resources:', error);
    }
  };

  useEffect(() => {
    fetchResourceList();
  }, []);

  const addNewResource = async (resource: Omit<Resource, 'id'>) => {
    try {
      const newResource = await createResource(resource);
      setResources((prevResources) => [...prevResources, newResource]);
      return newResource;
    } catch (error) {
      console.error('Error creating resource:', error);
      throw error;
    }
  };

  const updateExistingResource = async (id: string, resource: Omit<Resource, 'id'>) => {
    try {
      const updatedResource = await updateResource(id, resource);
      setResources((prevResources) =>
        prevResources.map((r) => (r.id === id ? updatedResource : r))
      );
      return updatedResource;
    } catch (error) {
      console.error('Error updating resource:', error);
      throw error;
    }
  };

  const removeResource = async (id: string) => {
    try {
      const success = await deleteResource(id);
      if (success) {
        setResources((prevResources) => prevResources.filter((r) => r.id !== id));
      }
      return success;
    } catch (error) {
      console.error('Error deleting resource:', error);
      throw error;
    }
  };

  return (
    <ResourceContext.Provider value={{
      resources,
      getResourceList: fetchResourceList,
      createResource: addNewResource,
      updateResource: updateExistingResource,
      deleteResource: removeResource,
    }}>
      {children}
    </ResourceContext.Provider>
  );
}

export default ResourceContextProvider;