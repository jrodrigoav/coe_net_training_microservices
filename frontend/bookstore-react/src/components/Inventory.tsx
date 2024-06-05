import React, { useEffect, useState, ChangeEvent } from 'react';
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
      <div className="container-fluid">
        <div className="row">
          <div className="col-md-12">
            <div className="card">
              <div className="header p-2">
                <div className="form-group">
                  <div className="input-group">
                    <div className="input-group-text">
                      <i className="bi bi-search"></i>
                    </div>
                    <input 
                      type="text" 
                      className="form-control" 
                      name="searchString" 
                      placeholder="Type to search..." 
                      value={searchString} 
                      onChange={handleSearch} 
                    />
                  </div>
                </div>
              </div>
              <div className="content table-responsive table-full-width">
                <table className="table table-hover table-striped">
                  <thead>
                    <tr>
                      <th>ID</th>
                      <th>Name</th>
                      <th>Available Copies</th>
                      <th>Unavailable Copies</th>
                      <th>Total</th>
                    </tr>
                  </thead>
                  <tbody>
                    {filteredInventory.map((row) => (
                      <tr key={row.resourceId}>
                        <td>{row.resourceId}</td>
                        <td>{row.resourceName}</td>
                        <td>{row.availableCopies}</td>
                        <td>{row.unavailableCopies}</td>
                        <td>{row.totalCopies}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default InventoryComponent;