import "./App.css";
import { useEffect, useState } from "react";
import axios from "axios";

function App() {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        axios.get("https://localhost:7246/api/User/getUsers").then((res) => {
            setUsers(res.data);
        });
    }, []);

    return (
        <div className="App">
            <div className="containerList">
                {users.map((user) => {
                    return (
                        <div key={user.id} className="container">
                            <h1 className="title">{user.firstName}</h1>
                            <p className="subtitle"> {user.lastName}</p>
                            <button className="button">Click me</button>
                        </div>
                    );
                })}
            </div>
        </div>
    );
}

export default App;
