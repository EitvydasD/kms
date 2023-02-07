// eslint-disable-next-line import/no-extraneous-dependencies
import { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
	appId: 'com.kms.app',
	appName: 'kms',
	webDir: 'dist/kms',
	bundledWebRuntime: false,
	server: {
		url: 'http://192.168.1.242:4200',
		cleartext: true,
	},
};

// eslint-disable-next-line import/no-default-export
export default config;
