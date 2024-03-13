create role ticket_admin;
create role azure_pg_admin;

--

-- PostgreSQL database dump
--

-- Dumped from database version 16.0
-- Dumped by pg_dump version 16.2 (Debian 16.2-1.pgdg120+2)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: public; Type: SCHEMA; Schema: -; Owner: azure_pg_admin
--



ALTER SCHEMA public OWNER TO azure_pg_admin;

--
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: azure_pg_admin
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: available_event; Type: TABLE; Schema: public; Owner: ticket_admin
--

CREATE TABLE public.available_event (
    id integer NOT NULL,
    start_time timestamp without time zone,
    name character varying(50)
);


ALTER TABLE public.available_event OWNER TO ticket_admin;

--
-- Name: ticket; Type: TABLE; Schema: public; Owner: ticket_admin
--

CREATE TABLE public.ticket (
    id integer NOT NULL,
    event_id integer,
    scanned boolean,
    identifier character varying(100)
);


ALTER TABLE public.ticket OWNER TO ticket_admin;

--
-- Data for Name: available_event; Type: TABLE DATA; Schema: public; Owner: ticket_admin
--

COPY public.available_event (id, start_time, name) FROM stdin;
1	2024-02-13 17:01:27.342377	Imagine Dragons
2	2024-02-14 17:01:27.342377	Daft Punk
3	2024-02-15 17:01:27.342377	Taylor Swift
4	2024-02-16 17:01:27.342377	Billy Ray Cyrus
5	2024-02-17 17:01:27.342377	Will Smith
\.


--
-- Data for Name: ticket; Type: TABLE DATA; Schema: public; Owner: ticket_admin
--

COPY public.ticket (id, event_id, scanned, identifier) FROM stdin;
1	1	f	1184303393877554543334
2	2	f	2360774211515167965633
3	3	f	18214185162050985360804
4	4	f	1609405978416590516735
5	5	f	19939778232035699227241
6	5	f	1491473655913965450342
7	1	f	623951590412801153303
8	1	f	64002030588868199316
9	1	f	6883856371211420291583
10	5	f	17344424611560196873628
11	1	f	10259352541412018225745
12	5	f	14112531691064917746321
13	4	f	953179977981371293446
\.


--
-- Name: available_event available_event_pkey; Type: CONSTRAINT; Schema: public; Owner: ticket_admin
--

ALTER TABLE ONLY public.available_event
    ADD CONSTRAINT available_event_pkey PRIMARY KEY (id);


--
-- Name: ticket ticket_pkey; Type: CONSTRAINT; Schema: public; Owner: ticket_admin
--

ALTER TABLE ONLY public.ticket
    ADD CONSTRAINT ticket_pkey PRIMARY KEY (id);


--
-- Name: ticket ticket_event_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ticket_admin
--

ALTER TABLE ONLY public.ticket
    ADD CONSTRAINT ticket_event_id_fkey FOREIGN KEY (event_id) REFERENCES public.available_event(id);


--
-- PostgreSQL database dump complete
--

