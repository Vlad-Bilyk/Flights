const fs = require('fs');
const filePath = './src/app/api/models.ts';

let content = fs.readFileSync(filePath, 'utf8');
content = content.replace(/export {(.*?)} from/g, 'export type {$1} from');

fs.writeFileSync(filePath, content);
console.log('Export statements updated successfully!');
