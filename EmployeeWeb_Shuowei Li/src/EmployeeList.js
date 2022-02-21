import React, { Component } from 'react';
import { Button, ButtonGroup, Container, Table } from 'reactstrap';
import AppNavbar from './AppNavbar';
import { Link } from 'react-router-dom';

class EmployeeList extends Component {

    constructor(props) {
        super(props);
        this.state = { employees: [] };
        this.remove = this.remove.bind(this);
    }

    componentDidMount() {
        fetch('http://localhost:8080/employees')
            .then(
                response => response.json()
            )
            .then(
                data => this.setState({ employees: data }
                ));
    }

    async remove(id) {
        await fetch(`http://localhost:8080/employees/${id}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        }).then(() => {
            let updatedEmployees = [...this.state.employees].filter(i => i.id !== id);
            this.setState({ employees: updatedEmployees });
        });
    }

    render() {
        const { employees } = this.state;
        const employeeList = employees.map(employee => {
            return <tr key={employee.id}>
                <td style={{ whiteSpace: 'nowrap' }}>{employee.name}</td>
                <td>{employee.phone}</td>
                <td>{employee.department}</td>
                <td>{employee.street}</td>
                <td>{employee.city}</td>
                <td>{employee.state}</td>
                <td>{employee.zip}</td>
                <td>{employee.country}</td>
                <td>
                    <ButtonGroup>
                        <Button size="sm" color="primary" tag={Link} to={"/employees/" + employee.id}>Edit</Button>
                        <Button size="sm" color="danger" onClick={() => { alert("Delete success."); this.remove(employee.id) }}>Delete</Button>
                    </ButtonGroup>
                </td>
            </tr>
        });

        return (
            <div>
                <AppNavbar />
                <Container fluid>
                    <div className="float-right">
                        <Button color="success" tag={Link} to="/employees/new">Add Employee</Button>
                    </div>
                    <h3>Employees</h3>
                    <Table className="mt-4">
                        <thead>
                            <tr>
                                <th width="10%">Name</th>
                                <th width="10%">Phone</th>
                                <th width="20%">Department</th>
                                <th width="15%">Street</th>
                                <th width="5%">City</th>
                                <th width="5%">State</th>
                                <th width="5%">Zip</th>
                                <th width="10%">Country</th>
                                <th width="15%">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {employeeList}
                        </tbody>
                    </Table>
                </Container>
            </div>
        );
    }
}

export default EmployeeList;