import React, { useEffect, useState } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip } from 'recharts';
import { pythonApi } from '../../api/axios';


const CustomTooltip = ({ active, payload, label }) => {
  if (active && payload && payload.length) {
    const timestamp = new Date(label*1000);
    const localeTimeString = timestamp.toLocaleTimeString();

    const value = payload[0].value.toFixed(2).replace(".", ",");
    return (
      <div className="custom-tooltip bg-white border rounded border-black">
        <p className="label">{`${localeTimeString}`}</p>
        <p className="label">{`BUX: ${value}`}</p>
      </div>
    );
  }

  return null;
};

const LineChartForm = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    pythonApi.get('bux/today')
      .then(response => {        
        setData(response.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []);


  return (
    <LineChart className="Linechart" width={800} height={470} data={data} >
      <CartesianGrid strokeDasharray="5 3" />
      <XAxis dataKey="timestamp" interval={11} tickFormatter={(timestamp) => new Date(timestamp*1000).toLocaleTimeString()}/>
      <YAxis domain={['auto', 'auto']} />
      <Tooltip content={<CustomTooltip />} />
      <Line type="monotone" dataKey="value" stroke="#8884d8" activeDot={{ r: 5 }} />
    </LineChart>
  );
};

export default LineChartForm;



// const modifiedData = response.data.map(item => ({
        //   ...item,
        //   Time: `${item.Hour}:${item.Minute.toString().padStart(2, '0')}`
        // }));