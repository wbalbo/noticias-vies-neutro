import { useState } from 'react';
import { summarizeUrl } from '../lib/api';

export default function SummaryForm() {
  const [url, setUrl] = useState('');
  const [summary, setSummary] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    try {      
      const data = await summarizeUrl(url);
      setSummary(data.summary || JSON.stringify(data));
    } catch (err) {
      setError('Erro ao resumir a notícia.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-xl mx-auto mt-10 p-4">
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <input
          type="text"
          placeholder="Cole o link da notícia aqui"
          className="border p-2 rounded"
          value={url}
          onChange={(e) => {console.log(e.target.value); setUrl(e.target.value)}}
        />
        <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">
          Resumir
        </button>
      </form>

      {loading && <p className="mt-4 text-gray-500">Gerando resumo...</p>}
      {error && <p className="mt-4 text-red-600">{error}</p>}
      {summary && (
        <div className="mt-4 p-4 border rounded bg-gray-50">
          <h3 className="font-semibold mb-2">Resumo:</h3>
          <p>{summary}</p>
        </div>
      )}
    </div>
  );
}
