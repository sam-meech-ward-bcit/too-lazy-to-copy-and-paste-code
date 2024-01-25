import { useEffect, useState } from "react";
import "./App.css";

type Todo = {
  id: number;
  title: string;
  completed: boolean;
  createdAt: string;
};

export default function App() {
  const [todos, setTodos] = useState<Todo[]>([]);

  useEffect(() => {
    async function fetchTodos() {
      const response = await fetch("/api/todos");
      const data = await response.json();
      setTodos(data);
    }
    fetchTodos();
  }, []);

  return (
    <>
      <h1>Hello from react and dotnet</h1>
      <h2>Here's my list of todos</h2>
      {todos.map((todo) => (
        <div key={todo.id}>
          <h2>{todo.title}</h2>
          <p>{todo.completed ? "completed" : "not completed"}</p>
        </div>
      ))}
    </>
  );
}
