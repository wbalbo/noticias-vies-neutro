import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5162",
});

export const summarizeUrl = async (url) => {
  try {
    const response = await api.post("/api/NewsSummary", { url });
    return response.data;
  } catch (error) {
    console.error("Error calling /api/NewsSummary:", error);
    throw error;
  }
};
