import React from 'react';
import ReactDOM from 'react-dom';
import { PDFViewer } from '@react-pdf/renderer';
import Template from '../templates/Template';
import '../styles/components/viewerPDF.scss';

const PDFViewerr = () => (
  <PDFViewer className='viewer' >
    <Template />
  </PDFViewer>
);

export default (PDFViewerr);