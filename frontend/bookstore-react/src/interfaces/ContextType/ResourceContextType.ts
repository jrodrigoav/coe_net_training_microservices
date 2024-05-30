import { Resource } from "../Resource";


export interface ResourceContextType {
  resources: Resource[];
  getResourceList: () => Promise<void>;
  createResource: (resource: Omit<Resource, 'id'>) => Promise<Resource>;
  updateResource: (id: string, resource: Omit<Resource, 'id'>) => Promise<Resource>;
  deleteResource: (id: string) => Promise<boolean>;
}