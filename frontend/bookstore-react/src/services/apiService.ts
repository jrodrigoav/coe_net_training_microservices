import apiConfig from '../assets/apiConfig.json';

export function getResourceApiUrl(): string {
  return apiConfig.api.resourceApi;
}

export function getInventoryApiUrl(): string {
  return apiConfig.api.inventoryApi;
}

export function getClientApiUrl(): string {
  return apiConfig.api.clientApi;
}

export function getRentingApiUrl(): string {
  return apiConfig.api.rentingApi;
}
