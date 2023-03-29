/* Periodos de movimientos antes del periodo actual */
select * from [dbo].[Period] 
where PeriodYear < 2023 or 
(PeriodYear = 2023 and PeriodMonth <= 3);

/* 
  Todos los proyectos donde se han realizado movimientos en periodos anteriores al actual, 
  es decir, todos los projectId que estan por fuera podrian iniciar movimientos o flujo de cartera
  en los sgtes periodos
*/
select distinct ProjectId 
from [dbo].[ProjectMovement]
where PeriodId IN (
	select PeriodId 
	from [dbo].[Period] 
	where PeriodYear < 2023 or 
	(PeriodYear = 2023 and PeriodMonth <= 3));

/* Todos los proyectos donde se realizaron movimientos o flujo de cartera desde el periodo actual */
select IP.ProjectId
from [dbo].[InvestmentProject] IP
where IP.ProjectId not in (
	select distinct PM.ProjectId 
	from [dbo].[ProjectMovement] PM
	where PM.PeriodId IN (
		select P.PeriodId 
		from [dbo].[Period] P
		where P.PeriodYear < 2023 or 
		(P.PeriodYear = 2023 and P.PeriodMonth <= 3)));

/* 
  Todos los proyectos que cuyos flujos de cartera o movimientos comienzan en un periodo superior 
  al mes de realización de la prueba
*/
select IP.ProjectId
from [dbo].[InvestmentProject] IP
where IP.ProjectId not in (
	select distinct PM.ProjectId 
	from [dbo].[ProjectMovement] PM
	where PM.PeriodId IN (
		select P.PeriodId 
		from [dbo].[Period] P
		where P.PeriodYear < 2023 or 
		(P.PeriodYear = 2023 and P.PeriodMonth <= 3))) 
intersect
select distinct ProjectId 
from [dbo].[ProjectMovement]
where PeriodId IN (
	select PeriodId
	from [dbo].[Period] 
	where PeriodYear = 2023 and PeriodMonth = 4
);

-- Finaliza primera parte analisis

select * 
from [dbo].[InvestmentProject]
where ProjectId = 28095 and BrokerId = 135;
-- BrokerId: 135, projectId: 28095, InvestmentAmount: 36779484170.0000
-- 36779484170.0000 (Inversion Inicial)
--  9185216755.0000 (Primer flujo de cartera)

select * from [dbo].[ProjectMovement]
where ProjectId = 28095 and 
PeriodId IN (
	select PeriodId 
	from [dbo].[Period] 
	where PeriodYear = 2023 and PeriodMonth > 3)

with Valid_Projects as (
	select PM.ProjectId, PM.MovementAmount 
	from [dbo].[ProjectMovement] PM
	where PeriodId = 280 
	and PM.MovementAmount <= 0/*(
		select IP.InvestmentAmount
		from [dbo].[InvestmentProject] IP
		where IP.ProjectId = PM.ProjectId
	)*/
)
select distinct BrokerId, Valid_Projects.ProjectId
from [dbo].[InvestmentProject] a
inner join Valid_Projects on a.ProjectId = Valid_Projects.ProjectId
order by BrokerId;