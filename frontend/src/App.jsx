import SummaryForm from './components/SummaryForm';

function App() {
  return (
    <main className="min-h-screen flex flex-col items-center justify-center bg-gray-100 p-4">
      <h1 className="text-3xl font-bold text-center mb-4">Resumo de Notícias com Viés Neutro</h1>
      <SummaryForm />
    </main>
  );
}

export default App;
