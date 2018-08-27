'use strict';

Object.defineProperty(exports, "__esModule", {
  value: true
});

var _express = require('express');

var _express2 = _interopRequireDefault(_express);

var _masterWebDriver = require('./webDriver/masterWebDriver');

var _masterWebDriver2 = _interopRequireDefault(_masterWebDriver);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var app = (0, _express2.default)();

var port = 3000;

app.listen(port, function () {
  console.log('Started on port ', port);
  (0, _masterWebDriver2.default)();
});

exports.default = app;
//# sourceMappingURL=index.js.map