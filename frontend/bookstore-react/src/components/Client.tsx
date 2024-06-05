import React, { useEffect, useState, ChangeEvent } from 'react';
import { Button, Input, Table, Modal, Form, Select } from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
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
  const [modalVisible, setModalVisible] = useState<boolean>(false);
  const [modalTitle, setModalTitle] = useState<string>('');
  const [clientId, setClientId] = useState<string>('');
  const [rentingList, setRentingList] = useState<ClientRenting[]>([]);
  const [returnDate, setReturnDate] = useState<string>('');
  const [isRentingModalVisible, setIsRentingModalVisible] = useState<boolean>(false);
  const [isReturnModalVisible, setIsReturnModalVisible] = useState<boolean>(false);

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
    setModalVisible(false);
  };

  const submitRentingModal = async () => {
    await registerRenting(rentingForm);
    setIsRentingModalVisible(false);
  };

  const onNew = () => {
    setModalTitle("New Client");
    setClientId('');
    setClientForm({ id: '', firstName: '', lastName: '', email: '' });
    setModalVisible(true);
  };

  const onUpdate = (client: Client) => {
    setModalTitle("Update Client");
    setClientId(client.id);
    setClientForm(client);
    setModalVisible(true);
  };

  const onRenting = async (id: string) => {
    setClientId(id);
    setIsRentingModalVisible(true);
    const availableResources = await resources;
    if (availableResources.length > 0) {
      setRentingForm({ resourceId: availableResources[0].id, clientId: id, registrationDate: new Date().toISOString().substring(0, 10) });
    }
  };

  const onReturn = async (id: string) => {
    setClientId(id);
    const rentings = await listRentingsByClientId(id);
    setRentingList(rentings);
    setIsReturnModalVisible(true);
  };

  const handleReturnResource = async () => {
    if (clientId && returnDate) {
      await returnRentingResource(clientId, returnDate);
      setReturnDate('');
      setIsReturnModalVisible(false);
    }
  };

  return (
    <div className="main-content">
      <Button type="primary" icon={<PlusOutlined />} onClick={onNew} style={{ marginBottom: '20px' }}>
        New
      </Button>
      <Input
        placeholder="Type to search..."
        value={searchString}
        onChange={handleSearch}
        style={{ marginBottom: '20px' }}
      />
      <Table dataSource={filteredClients} rowKey="id">
        <Table.Column title="ID" dataIndex="id" key="id" />
        <Table.Column title="First Name" dataIndex="firstName" key="firstName" />
        <Table.Column title="Last Name" dataIndex="lastName" key="lastName" />
        <Table.Column title="Email" dataIndex="email" key="email" />
        <Table.Column
          title="Actions"
          key="actions"
          render={(record: Client) => (
            <div className="flex gap-2">
              <Button icon={<EditOutlined />} onClick={() => onUpdate(record)}>
                Edit
              </Button>
              <Button icon={<PlusOutlined />} onClick={() => onRenting(record.id)}>
                Renting
              </Button>
              <Button icon={<DeleteOutlined />} onClick={() => onReturn(record.id)}>
                Return
              </Button>
            </div>
          )}
        />
      </Table>

      <Modal
        title={modalTitle}
        visible={modalVisible}
        onCancel={() => setModalVisible(false)}
        footer={[
          <Button key="cancel" onClick={() => setModalVisible(false)}>Cancel</Button>,
          <Button key="submit" type="primary" onClick={submitClientModal}>Ok</Button>,
        ]}
      >
        <Form layout="vertical">
          <Form.Item label="First Name">
            <Input
              name="firstName"
              value={clientForm.firstName}
              onChange={handleClientFormChange}
            />
          </Form.Item>
          <Form.Item label="Last Name">
            <Input
              name="lastName"
              value={clientForm.lastName}
              onChange={handleClientFormChange}
            />
          </Form.Item>
          <Form.Item label="Email">
            <Input
              name="email"
              value={clientForm.email}
              onChange={handleClientFormChange}
            />
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        title="Register Renting"
        visible={isRentingModalVisible}
        onCancel={() => setIsRentingModalVisible(false)}
        footer={[
          <Button key="cancel" onClick={() => setIsRentingModalVisible(false)}>Cancel</Button>,
          <Button key="submit" type="primary" onClick={submitRentingModal}>Ok</Button>,
        ]}
      >
        <Form layout="vertical">
          <Form.Item label="Resource">
            <Select
              value={rentingForm.resourceId}
              onChange={(value) => setRentingForm({ ...rentingForm, resourceId: value })}
            >
              {resources.map((resource: Resource) => (
                <Select.Option key={resource.id} value={resource.id}>{resource.name}</Select.Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item label="Date">
            <Input
              type="date"
              name="registrationDate"
              value={rentingForm.registrationDate}
              onChange={handleRentingFormChange}
            />
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        title="Return"
        visible={isReturnModalVisible}
        onCancel={() => setIsReturnModalVisible(false)}
        footer={[
          <Button key="cancel" onClick={() => setIsReturnModalVisible(false)}>Cancel</Button>,
          <Button key="submit" type="primary" onClick={handleReturnResource}>Ok</Button>,
        ]}
      >
        <Table dataSource={rentingList} rowKey="id">
          <Table.Column title="Resource" dataIndex="resourceName" key="resourceName" />
          <Table.Column title="Registration Date" dataIndex="registrationDate" key="registrationDate" render={(date: string) => new Date(date).toLocaleDateString()} />
          <Table.Column
            title="Return"
            key="return"
            render={(record: ClientRenting) => (
              <Button type="link" onClick={() => setClientId(record.id)}>
                Return
              </Button>
            )}
          />
        </Table>
        <Form layout="vertical">
          <Form.Item label="Return Date">
            <Input
              type="date"
              value={returnDate}
              onChange={(e) => setReturnDate(e.target.value)}
            />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default ClientComponent;