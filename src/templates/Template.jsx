import React from 'react';
import { Page, Text, View, Document, StyleSheet } from '@react-pdf/renderer';

// Create styles
const styles = StyleSheet.create({
  page: {
	display: 'flex',
    flexDirection: 'row',
    backgroundColor: '#E4E4E4',
  },
  section: {
    margin: 10,
    padding: 10,
    flexGrow: 1
  }
});

// Create Document Component
const Template = ({ name = '', surname = '', email = '', phone = '', location = '', bio = '' }) => (
	<Document>
		<Page size="A4" style={styles.page}>
			<View style={styles.section} >
				<Text>
					{name == '' ? 'Jonathan ' : name + ' '}
					{surname == '' ? 'Doe' : surname}
				</Text>
				<View style={styles.section}>
					<Text>{email == '' ? 'name@yourdomain.com' : email} </Text>
					<Text>{phone == '' ? '+48 123 456 789' : phone}</Text>
				</View >
				<View style={styles.section}>
					<Text>{location == '' ? 'Warsaw, Poland' : location} </Text>
					<Text>{bio == '' ? 'Web-designer' : bio}</Text>
				</View >
			</View>
		</Page>


	</Document>

);

export default Template;