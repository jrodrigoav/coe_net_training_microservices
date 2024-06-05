import React, { useEffect, useState, ChangeEvent } from 'react';
import { useClientContext } from '../contexts/ClientContext';
import { useResourceContext } from '../contexts/ResourceContext';
import { useRentingContext } from '../contexts/RentingContext';
import { Client } from '../interfaces/Client';
import { ClientRenting } from '../interfaces/Client-renting';
import { Resource } from '../interfaces/Resource';

const ClientComponent: React.FC = () => {
  const { clients, getClients, createClient, updateClient } = useClientContext();
  const { resources } = useResourceContext();
  const { registerRenting, returnRentingResource, listRentingsByClientId } = useRentingContext();

  const [searchString, setSearchString] = useState<string>('');
  const [filteredClients, setFilteredClients] = useState<Client[]>([]);
  const [clientForm, setClientForm] = useState<Client>({ id: '', firstName: '', lastName: '', email: '' });
  const [rentingForm, setRentingForm] = useState<{ resourceId: string, clientId: string, registrationDate: string }>({ resourceId: '', clientId: '', registrationDate: new Date().toISOString().substring(0, 10) });
  const [modalTitle, setModalTitle] = useState<string>('');
  const [clientId, setClientId] = useState<string>('');
  const [rentingList, setRentingList] = useState<ClientRenting[]>([]);
  const [returnDate, setReturnDate] = useState<string>('');

  useEffect(() => {
    getClients();
  }, [getClients]);

  useEffect(() => {
    setFilteredClients(clients.filter(client => client.firstName.toLowerCase().includes(searchString.toLowerCase())));
  }, [clients, searchString]);

  const handleSearch = (e: ChangeEvent<HTMLInputElement>) => {
    setSearchString(e.target.value);
  };

  const handleClientFormChange = (e: ChangeEvent<HTMLInputElement>) => {
    setClientForm({ ...clientForm, [e.target.name]: e.target.value });
  };

  const handleRentingFormChange = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setRentingForm({ ...rentingForm, [e.target.name]: e.target.value });
  };

  const submitClientModal = async () => {
    if (clientId) {
      await updateClient(clientId, clientForm);
    } else {
      await createClient(clientForm);
    }
    getClients();
  };

  const submitRentingModal = async () => {
    await registerRenting(rentingForm);
  };

  const onNew = () => {
    setModalTitle("New Client");
    setClientId('');
    setClientForm({ id: '', firstName: '', lastName: '', email: '' });
  };

  const onUpdate = (client: Client) => {
    setModalTitle("Update Client");
    setClientId(client.id);
    setClientForm(client);
  };

  const onRenting = async (id: string) => {
    setClientId(id);
    const availableResources = await resources;
    if (availableResources.length > 0) {
      setRentingForm({ resourceId: availableResources[0].id, clientId: id, registrationDate: new Date().toISOString().substring(0, 10) });
    }
  };

  const onReturn = async (id: string) => {
    setClientId(id);
    const rentings = await listRentingsByClientId(id);
    setRentingList(rentings);
  };

  const handleReturnResource = async () => {
    if (clientId && returnDate) {
      await returnRentingResource(clientId, returnDate);
      setReturnDate('');
    }
  };

  return (
    <div className="main-content">
      <div className="container-fluid">
        <div className="row">
          <div className="col-md-12">
            <div className="card">
              <div className="header d-flex flex-column gap-2 p-2">
                <button type="button" data-bs-toggle="modal" data-bs-target="#newModal" data-bs-backdrop="false" onClick={onNew} className="btn btn-success">New +</button>
                <div className="form-group">
                  <div className="input-group padding-top">
                    <div className="input-group-text">
                      <i className="bi bi-search"></i>
                    </div>
                    <input type="text" className="form-control" name="searchString" placeholder="Type to search..." value={searchString} onChange={handleSearch} />
                  </div>
                </div>
              </div>
              <div className="content table-responsive table-full-width">
                <table className="table table-hover table-striped">
                  <thead>
                    <tr>
                      <th>ID</th>
                      <th>First Name</th>
                      <th>Last Name</th>
                      <th>Email</th>
                      <th>Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    {filteredClients.map((client) => (
                      <tr key={client.id}>
                        <td>{client.id}</td>
                        <td>{client.firstName}</td>
                        <td>{client.lastName}</td>
                        <td>{client.email}</td>
                        <td>
                          <button type="button" data-bs-toggle="modal" data-bs-target="#newModal" onClick={() => onUpdate(client)} data-bs-backdrop="false" className="btn btn-info">Edit <i className="bi bi-pencil"></i></button>
                          <button type="button" data-bs-toggle="modal" data-bs-target="#rentingModal" onClick={() => onRenting(client.id)} data-bs-backdrop="false" className="btn btn-secondary">Renting <i className="bi bi-file-earmark-font"></i></button>
                          <button type="button" data-bs-toggle="modal" data-bs-target="#returnModal" onClick={() => onReturn(client.id)} data-bs-backdrop="false" className="btn btn-warning">Return <i className="bi bi-arrow-counterclockwise"></i></button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* New Client Modal */}
      <div className="modal" id="newModal">
        <div className="modal-dialog">
          <div className="modal-content">
            <div className="modal-header">
              <h4 className="modal-title">{modalTitle}</h4>
            </div>
            <div className="modal-body">
              <form>
                <div className="row gy-2">
                  <div className="form-group col-xs-12">
                    <label htmlFor="firstName">First Name:</label>
                    <input type="text" name="firstName" className="form-control" placeholder="Name" value={clientForm.firstName} onChange={handleClientFormChange} />
                  </div>
                  <div className="form-group col-xs-12">
                    <label htmlFor="lastName">Last Name:</label>
                    <input type="text" name="lastName" className="form-control" placeholder="Last Name" value={clientForm.lastName} onChange={handleClientFormChange} />
                  </div>
                  <div className="form-group col-xs-12">
                    <label htmlFor="email">Email:</label>
                    <input type="email" name="email" className="form-control" placeholder="Email" value={clientForm.email} onChange={handleClientFormChange} />
                  </div>
                </div>
              </form>
            </div>
            <div className="modal-footer">
              <button type="button" className="btn btn-primary" onClick={submitClientModal} data-bs-dismiss="modal">Ok</button>
              <button type="button" className="btn btn-danger" data-bs-dismiss="modal">Cancel</button>
            </div>
          </div>
        </div>
      </div>

      {/* Renting Modal */}
      <div className="modal" id="rentingModal">
        <div className="modal-dialog">
          <div className="modal-content">
            <div className="modal-header">
              <h4 className="modal-title">Register Renting</h4>
            </div>
            <div className="modal-body">
              <form>
                <div className="row gy-2">
                  <div className="form-group col-xs-12">
                    <label htmlFor="resourceId">Resource:</label>
                    <select className="form-control" name="resourceId" value={rentingForm.resourceId} onChange={handleRentingFormChange}>
                      {resources.map((resource: Resource) => (
                        <option key={resource.id} value={resource.id}>{resource.name}</option>
                      ))}
                    </select>
                  </div>
                  <div className="form-group col-xs-12">
                    <label htmlFor="registrationDate">Date:</label>
                    <input type="date" name="registrationDate" className="form-control" value={rentingForm.registrationDate} onChange={handleRentingFormChange} />
                  </div>
                </div>
              </form>
            </div>
            <div className="modal-footer">
              <button type="button" className="btn btn-primary" onClick={submitRentingModal} data-bs-dismiss="modal">Ok</button>
              <button type="button" className="btn btn-danger" data-bs-dismiss="modal">Cancel</button>
            </div>
          </div>
        </div>
      </div>

      {/* Return Modal */}
      <div className="modal fade" id="returnModal">
        <div className="modal-dialog">
          <div className="modal-content">
            <div className="modal-header">
              <h4 className="modal-title">Return</h4>
            </div>
            <div className="modal-body">
              <div className="row gy-2">
                <div className="col-md-12">
                  <div className="card">
                    <div className="content table-responsive table-full-width">
                      <table className="table table-hover table-striped">
                        <thead>
                          <tr>
                            <th>Resource</th>
                            <th>Registration Date</th>
                          </tr>
                        </thead>
                        <tbody>
                          {rentingList.map((renting) => (
                            <tr key={renting.id}>
                              <td>{renting.resourceName}</td>
                              <td>{new Date(renting.registrationDate).toLocaleDateString()}</td>
                              <td>
                                <button type="button" data-bs-toggle="modal" data-bs-target="#dateModal" onClick={() => setClientId(renting.id)} data-bs-backdrop="false" className="btn btn-info" data-bs-dismiss="modal">
                                  Return <i className="bi bi-arrow-counterclockwise"></i>
                                </button>
                              </td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div className="modal-footer">
              <button type="button" className="btn btn-danger" data-bs-dismiss="modal">Cancel</button>
            </div>
          </div>
        </div>
      </div>

      {/* Date Modal */}
      <div className="modal fade" id="dateModal">
        <div className="modal-dialog">
          <div className="modal-content">
            <div className="modal-header">
              <h4 className="modal-title">Select Return Date</h4>
            </div>
            <div className="modal-body">
              <div className="row gy-2">
                <div className="form-group col-xs-12">
                  <label>Date:</label>
                  <input type="date" className="form-control" value={returnDate} onChange={(e) => setReturnDate(e.target.value)} />
                </div>
              </div>
            </div>
            <div className="modal-footer">
              <button type="button" className="btn btn-primary" onClick={handleReturnResource} data-bs-dismiss="modal" disabled={!returnDate}>Ok</button>
              <button type="button" className="btn btn-danger" data-bs-dismiss="modal">Cancel</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ClientComponent;