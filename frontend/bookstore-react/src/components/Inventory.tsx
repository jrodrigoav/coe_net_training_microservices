import React, { useEffect, useState, ChangeEvent } from 'react';
import { Input, Table } from 'antd';
import { useInventoryContext } from '../contexts/InventoryContext';
import { InventorySummary } from '../interfaces/Inventory-summary';

const InventoryComponent: React.FC = () => {
  const { inventorySummary, getInventorySummary } = useInventoryContext();
  const [searchString, setSearchString] = useState<string>('');
  const [filteredInventory, setFilteredInventory] = useState<InventorySummary[]>([]);

  useEffect(() => {
    getInventorySummary();
  }, [getInventorySummary]);

  useEffect(() => {
    setFilteredInventory(inventorySummary.filter(item => 
      item.resourceName.toLowerCase().includes(searchString.toLowerCase())
    ));
  }, [inventorySummary, searchString]);

  const handleSearch = (e: ChangeEvent<HTMLInputElement>) => {
    setSearchString(e.target.value);
  };

  return (
    <div className="main-content">
      <Input
        placeholder="Type to search..."
        value={searchString}
        onChange={handleSearch}
        style={{ marginBottom: '20px' }}
      />
      <Table dataSource={filteredInventory} rowKey="resourceId">
        <Table.Column title="ID" dataIndex="resourceId" key="resourceId" />
        <Table.Column title="Name" dataIndex="resourceName" key="resourceName" />
        <Table.Column title="Available Copies" dataIndex="availableCopies" key="availableCopies" />
        <Table.Column title="Unavailable Copies" dataIndex="unavailableCopies" key="unavailableCopies" />
        <Table.Column title="Total" dataIndex="totalCopies" key="totalCopies" />
      </Table>
    </div>
  );
};

export default InventoryComponent;