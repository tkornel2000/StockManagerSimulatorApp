import React, { useState, useEffect } from 'react';
import { pythonApi } from '../../api/axios';

function BuxPrice() {
  const [buxData, setBuxData] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await pythonApi.get('bux/current');
        const data = response.data;
        setBuxData(data);
      } catch (error) {
        console.log('Error fetching bux data:', error);
      }
    };

    fetchData();

    const interval = setInterval(() => {
      fetchData();
    }, 60000); // 1 perc = 60000 ms

    return () => {
      clearInterval(interval);
    };
  }, []);

  //const price = buxData.value ? buxData.value.toLocaleString() : null;
  const lastUpdate = new Date(buxData.timestamp*1000);
  const now = new Date();


  return (
    <div>
      <div className="">
        Utolsó adat ekkor: {lastUpdate.toLocaleDateString()}{"   "}{lastUpdate.toLocaleTimeString()}
      </div>
      <div>
        Piac állapota:{" "} 
        <span className={lastUpdate > now - 1200000 ? 'text-success' : 'text-danger'}>
          {lastUpdate > now - 1500000 ? 'Nyitva' : 'Zárva'}
        </span>
      </div>
      <div className='mb-3' style={{fontSize:"25px"}}>{buxData.value}</div>
    </div>
  );
}

export default BuxPrice;
