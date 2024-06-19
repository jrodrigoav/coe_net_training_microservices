import React from 'react';
import ClientContextProvider from './ClientContext';
import InventoryContextProvider from './InventoryContext';
import ResourceContextProvider from './ResourceContext';
import RentingContextProvider from './RentingContext';

const AppProviders: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <ClientContextProvider>
      <InventoryContextProvider>
        <ResourceContextProvider>
        <RentingContextProvider>
            {children}
          </RentingContextProvider>
        </ResourceContextProvider>
      </InventoryContextProvider>
    </ClientContextProvider>
  );
}

export default AppProviders;
