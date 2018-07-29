import 'chromedriver'
import chrome from 'selenium-webdriver/chrome'
import { Builder, By, Key } from 'selenium-webdriver'
import versionSite from '../versionSite/version'

// const URL = 'https://www.jusbrasil.com.br/jurisprudencia/busca?q=assédio+Belo+Horizonte+2017'
const URL = 'https://www.jusbrasil.com.br'

const chromeOptions = new chrome.Options()
chromeOptions.addArguments('test-type')
chromeOptions.addArguments('start-maximized')
chromeOptions.addArguments('--js-flags=--expose-gc')
chromeOptions.addArguments('--enable-precise-memory-info')
chromeOptions.addArguments('--disable-popup-blocking')
chromeOptions.addArguments('--disable-default-apps')
chromeOptions.addArguments('--disable-infobars')
chromeOptions.addArguments('--incognito')
chromeOptions.addArguments('--disable-extensionss')
chromeOptions.addArguments('--no-sandbox')
chromeOptions.addArguments('test-type=browser')

const driver = new Builder()
  .forBrowser('chrome')
  .withCapabilities(chromeOptions)
  .build()
var VERSION = ''

const MasterWebDriver = () => {
  initializaeBrowser()
  fetchUrl()
  // versionSite(VERSION)
}

const initializaeBrowser = async () => {
  try {
    await driver.manage().deleteAllCookies()
  } catch (error) {
    console.log('Server error: Ocorreu um erro inesperado na inicialização do arquivo binário respectivo ao navegador.'
    )
    console.log('Erro interno: ', error)
    await driver.close()
  }
}

const fetchUrl = async () => {
  try {
    await driver.get(URL)
    console.log('Success: ', URL)
    Steps()
  } catch (ERROR) {
    console.log('ERROR: ', ERROR)
  } finally {
    // await driver.close()
  }
}

const Steps = async () => {
  await sign()
  await setTimeout(() => {
    customSearchConfig()
  }, 3000)
}

const sign = async () => {
  // SignIn no JusBrasil
  await driver.findElement(By.className('btn btn--flat btn-login')).click()
  await driver.findElement(By.id('form-sign-up-email')).sendKeys('amigabilidade.genero.ifmg@gmail.com')
  await driver.findElement(By.id('form-sign-up-password')).sendKeys('@amigabilidade#genero')
  await driver.findElement(By.name('sign-up-button')).submit()
}

const customSearchConfig = async () => {
  // Seleciona Jurisprudência
  var customSearch = await driver.findElements(By.className('navbar-link'))
  await customSearch[3].click()
  // Limpa opções de órgãos judiciais
  await driver.findElement(By.id('radio-clear')).click()
  // Seleciona tribunal
  customSearch = await driver.findElements(By.className('dropdown-toggle'))
  await customSearch[2].click()
  await driver.findElement(By.id('10000575')).click()
  await customSearch[2].click()
  // Argumentos e busca
  search()
}

const search = async () => {
  var keys = 'assédio sexual'
  var btn = driver.findElement(By.name('q'))
  btn.sendKeys(keys, Key.ENTER)
}

const initV1 = () => {
  try {
    var searchForm = driver.findElement(By.className('title small'))
    searchForm = searchForm.findElement(By.tagName('a'))
    searchForm.click()
    searchForm = searchForm.findElement(By.className('JurisprudenceDecisionTabs-item btn active'))
    searchForm.click()
  } catch (ERROR) {
    console.log('Old version faill')
    console.log('Call new version')
    initV2()
  }
}

const initV2 = async () => {
  try {
    console.log('New version change')
    var searchForm = await driver.findElement(By.className('BaseSnippetWrapper-title'))
    searchForm = searchForm.findElement(By.tagName('a'))
    searchForm.click()
  } catch (ERROR) {
    console.log('New version faill')
    console.log('ERROR: ', ERROR)
  }
}

export default MasterWebDriver
