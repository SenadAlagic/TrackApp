import { useEffect, useRef, useState } from "react";

export const useCache = (api: string) => {
  const cacheData = useRef<{ [key: string]: any }>({});
  const [status, setStatus] = useState("idle");
  const [error, setError] = useState<string>("");
  const [data, setData] = useState([]);

  useEffect(() => {
    let revokeRequest = false;
    if (!api || !api.trim()) return;

    const renderData = async () => {
      setStatus("fetching");
      if (cacheData.current[api]) {
        const cachedData = cacheData.current[api];
        setData(cachedData);
        setStatus("fetched");
      } else {
        try {
          const response = await fetch(api);
          const fetchedData = await response.json();
          cacheData.current[api] = fetchedData;
          if (revokeRequest) return;
          setData(fetchedData);
          setStatus("fetched");
        } catch (error: any) {
          if (revokeRequest) return;
          setError(error.message);
          setStatus("error");
        }
      }
    };

    renderData();

    return function cleanup() {
      revokeRequest = true;
    };
  }, [api]);

  return { status, error, data };
};
