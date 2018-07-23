'use strict';

Object.defineProperty(exports, "__esModule", {
  value: true
});

var _express = require('express');

var _express2 = _interopRequireDefault(_express);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var app = (0, _express2.default)();

var port = 3000;

app.listen(port, function () {
  console.log('Started on port ${port}');
});

exports.default = app;

//  import 'chromedriver';
// // var webdriver = require('selenium-webdriver');
// // var driver = new webdriver.Builder()
// //   .forBrowser('chrome')
// //   .build();

// //   driver.manage().deleteAllCookies()

// //   driver.get('https://google.com')
// //   driver.close()

// import { Builder, By, Key, until } from 'selenium-webdriver';

// (async function example() {
//   let driver = await new Builder().forBrowser('chrome').build();
//   try {
//     await driver.get('https://www.google.com');
//     await driver.findElement(By.name('q')).sendKeys('webdriver', Key.RETURN);
//     await driver.wait(until.titleIs('webdriver - Google Search'), 1000);
//   } finally {
//     await driver.quit();
//   }
// })();
//# sourceMappingURL=index.js.map