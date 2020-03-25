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

## AOPSample006
使用 `.Net Framework` 的 `Remoting` 裡， `RealProxy` 和 `ProxyAttribute` 來實現 `AOP`

## AOPSample007
使用 `.Net Core` 的 `DispatchProxy` 實現 `AOP`

## AOPSample008
使用 `.Net Framework 4.6` 的 `DispatchProxy` 來實現 `AOP`
> Install-Nuget `System.Reflection.DispatchProxy`  
> 有版本限制，需 `.Net Framework 4.6` 以上  

## AOPSample009
利用 `DispatchProxy` 實現簡易 `AOP`

## 資料來源：
[AOP by Castle.Windsor](https://dotblogs.com.tw/hatelove/2014/05/04/implementation-aop-by-castle_windsor)  
[實現 AOP 的幾種方式](https://www.cnblogs.com/zuowj/p/7501896.html)  
[DispatchProxy實現簡單AOP]( https://www.cnblogs.com/elderjames/p/implement-simple-aop-using-a-dotnet-core-library-system-reflection-dispatchproxy.html )
