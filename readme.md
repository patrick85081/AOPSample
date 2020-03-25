# AOP Sample
## AOPSample001
使用 `Decorator` 模式，為現有的類別加入紀錄Log功能

## AOPSample002
使用 `WindsorContainer` 類別，用 `DI` 的方式註冊 `Decorator`
> Install-Nuget `Castle.Core`  
> Install-Nuget `Castle.Windsor`  

## AOPSample003
撰寫 `Interceptor` 來實現 `AOP` 動態代理，增加一個 `Customer` 類別套用效果
> Install-Nuget `Castle.Core`  
> Install-Nuget `Castle.Windsor`  

## AOPSample004
增加 `InterceptorSelector` 判斷每一個方法是否需要攔截
> Install-Nuget `Castle.Core`  
> Install-Nuget `Castle.Windsor`  

## AOPSample005
使用 `ProxyGenerator` 產生 動態代理 (不使用 `WindsorContainer` 註冊的方式)
> Install-Nuget `Castle.Core`  
> Install-Nuget `Castle.Windsor`  

## 資料來源：
[AOP by Castle.Windsor](https://dotblogs.com.tw/hatelove/2014/05/04/implementation-aop-by-castle_windsor)
