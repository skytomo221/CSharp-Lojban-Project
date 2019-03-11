using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LojbanTest
{

    public class Property
    {
        public string Key { get; private set; }
        public int Value { get; private set; }
        public Property(string key, int value)
        {
            Key = key;
            Value = value;
        }
    }

    public abstract class MetaNode
    {
    }

    // text <- intro-null NAI-clause* text-part-2 (!text-1 joik-jek)? text-1? faho-clause EOF?
    public class Text : MetaNode
    {
        public Intro_null Intro_null { get; set; }
        public IEnumerable<NAI_clause> NAI_clause { get; set; }
        public Text_part_2 Text_part_2 { get; set; }
        public Joik_jek Joik_jek { get; set; }
        public Text_1 Text_1 { get; set; }
        public Faho_clause Faho_clause { get; set; }
        public EOF EOF { get; set; }
    }

    // intro-null <- spaces? su-clause* intro-si-clause
    public class Intro_null : MetaNode
    {
        public Spaces Spaces { get; set; }
        public IEnumerable<Su_clause> Su_clause { get; set; }
        public Intro_si_clause Intro_si_clause { get; set; }
    }

    // text-part-2 <- (CMENE-clause+ / indicators?) free*
    public class Text_part_2 : MetaNode
    {
        public IEnumerable<CMENE_clause> CMENE_clause { get; set; }
        public Indicators Indicators { get; set; }
        public IEnumerable<Free> Free { get; set; }
    }

    //; intro-sa-clause <- SA-clause+ / any-word-SA-handling !(ZEI-clause SA-clause) intro-sa-clause
    // intro-si-clause <- si-clause? SI-clause*
    public class Intro_si_clause : MetaNode
    {
        public Si_clause Si_clause { get; set; }
        public IEnumerable<SI_clause> SI_clause { get; set; }

    }
    // faho-clause<- (FAhO-clause dot-star)?
    public class Faho_clause : MetaNode
    {
        public FAhO_clause FAhO_clause { get; set; }
        public Dot_star Dot_star { get; set; }
    }

    //; Please note that the "text-1" item in the text-1 production does
    //; *not* match the BNF.This is due to a bug in the BNF.  The change
    //; here was made to match grammar.300
    // text-1 <- I-clause (jek / joik)? (stag? BO-clause)? free* text-1? / NIhO-clause+ free* su-clause* paragraphs? / paragraphs
    public class Text_1 : MetaNode
    {
        public I_clause I_clause { get; set; }
        public Jek Jek { get; set; }
        public Joik Joik { get; set; }
        public Stag Stag { get; set; }
        public BO_clause BO_clause { get; set; }
        public IEnumerable<Free> Free { get; set; }
        public Text_1 Text_1_Child { get; set; }
        public IEnumerable<NIhO_clause> NIhO_clause { get; set; }
        public IEnumerable<Su_clause> Su_clause { get; set; }
        public Paragraphs Paragraphs { get; set; }
    }

    // paragraphs <- paragraph (NIhO-clause+ free* su-clause* paragraphs)?
    public class Paragraphs : MetaNode { }

    // paragraph <- (statement / fragment) (I-clause !jek !joik !joik-jek free* (statement / fragment)?)*
    public class Paragraph : MetaNode { }

    // statement<- statement-1 / prenex statement
    public class Statement : MetaNode { }

    // statement-1 <- statement-2 (I-clause joik-jek statement-2?)*
    public class Statement_1 : MetaNode { }

    // statement-2 <- statement-3 (I-clause(jek / joik)? stag? BO-clause free* statement-2)? / statement-3 (I-clause(jek / joik)? stag? BO-clause free*)?
    public class Statement_2 : MetaNode { }

    // statement-3 <- sentence / tag? TUhE-clause free* text-1 TUhU-clause? free*
    public class Statement_3 : MetaNode { }

    // fragment <- prenex / terms VAU-clause? free* / ek free* / gihek free* / quantifier / NA-clause !JA-clause free* / relative-clauses / links / linkargs
    public class Fragment : MetaNode { }

    // prenex <- terms ZOhU-clause free*
    public class Prenex : MetaNode { }

    //; sentence <- (terms CU-clause? free*)? bridi-tail / bridi-tail
    // sentence <- (terms bridi-tail-sa* CU-clause? free*)? bridi-tail-sa* bridi-tail
    public class Sentence : MetaNode { }

    // sentence-sa <- sentence-start (!sentence-start(sa-word / SA-clause !sentence-start ) )* SA-clause &text-1
    public class Sentence_sa : MetaNode { }

    // sentence-start <- I-pre / NIhO-pre
    public class Sentence_start : MetaNode { }

    // subsentence <- sentence / prenex subsentence
    public class Subsentence : MetaNode { }

    // bridi-tail <- bridi-tail-1 (gihek stag? KE-clause free* bridi-tail KEhE-clause? free* tail-terms)?
    public class Bridi_tail : MetaNode { }

    // bridi-tail-sa <- bridi-tail-start (term / !bridi-tail-start(sa-word / SA-clause !bridi-tail-start ) )* SA-clause &bridi-tail
    public class Bridi_tail_sa : MetaNode { }

    // bridi-tail-start <- ME-clause / NUhA-clause / NU-clause / NA-clause !KU-clause / NAhE-clause !BO-clause / selbri / tag bridi-tail-start / KE-clause bridi-tail-start / bridi-tail
    public class Bridi_tail_start : MetaNode { }

    // bridi-tail-1 <- bridi-tail-2 (gihek !(stag? BO-clause) !(stag? KE-clause) free* bridi-tail-2 tail-terms)*
    public class Bridi_tail_1 : MetaNode { }

    // bridi-tail-2 <- bridi-tail-3 (gihek stag? BO-clause free* bridi-tail-2 tail-terms)?
    public class Bridi_tail_2 : MetaNode { }

    // bridi-tail-3 <- selbri tail-terms / gek-sentence
    public class Bridi_tail_3 : MetaNode { }

    // gek-sentence <- gek subsentence gik subsentence tail-terms / tag? KE-clause free* gek-sentence KEhE-clause? free* / NA-clause free* gek-sentence
    public class Gek_sentence : MetaNode { }

    // tail-terms <- terms? VAU-clause? free*
    public class Tail_terms : MetaNode { }

    // terms<- terms-1+
    public class Terms : MetaNode { }

    // #; terms-1 <- terms-2 (PEhE-clause free* joik-jek terms-2)*

    // #; terms-2 <- term (CEhE-clause free* term)*

    // terms-1 <- terms-2 (pehe-sa* PEhE-clause free* joik-jek terms-2)*
    public class Terms_1 : MetaNode { }

    // terms-2 <- term (cehe-sa* CEhE-clause free* term)*
    public class Terms_2 : MetaNode { }

    // pehe-sa <- PEhE-clause (!PEhE-clause (sa-word / SA-clause !PEhE-clause))* SA-clause
    public class Pehe_sa : MetaNode { }

    // cehe-sa <- CEhE-clause (!CEhE-clause (sa-word / SA-clause !CEhE-clause))* SA-clause
    public class Cehe_sa : MetaNode { }

    // #;term <- sumti / ( !gek (tag / FA-clause free*) (sumti / KU-clause? free*) ) / termset / NA-clause KU-clause free*

    // term <- term-sa* term-1
    public class Term : MetaNode { }

    // term-1 <- sumti / ( !gek (tag / FA-clause free*) (sumti / KU-clause? free*) ) / termset / NA-clause KU-clause free*
    public class Term_1 : MetaNode { }

    // term-sa <- term-start (!term-start (sa-word / SA-clause !term-start ) )* SA-clause &term-1
    public class Term_sa : MetaNode { }

    // term-start <- term-1 / LA-clause / LE-clause / LI-clause / LU-clause / LAhE-clause / quantifier term-start / gek sumti gik / FA-clause / tag term-start
    public class Term_start : MetaNode { }

    // termset <- gek-termset / NUhI-clause free* gek terms NUhU-clause? free* gik terms NUhU-clause? free* / NUhI-clause free* terms NUhU-clause? free*
    public class Termset : MetaNode { }

    // gek-termset <- gek terms-gik-terms
    public class Gek_termset : MetaNode { }

    // terms-gik-terms <- term (gik / terms-gik-terms) term
    public class Terms_gik_terms : MetaNode { }

    // sumti <- sumti-1 (VUhO-clause free* relative-clauses)?
    public class Sumti : MetaNode { }

    // sumti-1 <- sumti-2 (joik-ek stag? KE-clause free* sumti KEhE-clause? free*)?
    public class Sumti_1 : MetaNode { }

    // sumti-2 <- sumti-3 (joik-ek sumti-3)*
    public class Sumti_2 : MetaNode { }

    // sumti-3 <- sumti-4 (joik-ek stag? BO-clause free* sumti-3)?
    public class Sumti_3 : MetaNode { }

    // sumti-4 <- sumti-5 / gek sumti gik sumti-4
    public class Sumti_4 : MetaNode { }

    // sumti-5 <- quantifier? sumti-6 relative-clauses? / quantifier selbri KU-clause? free* relative-clauses?
    public class Sumti_5 : MetaNode { }

    // sumti-6 <- ZO-clause free* / ZOI-clause free* / LOhU-clause free* / lerfu-string !MOI-clause BOI-clause? free* / LU-clause text LIhU-clause? free* / (LAhE-clause free* / NAhE-clause BO-clause free*) relative-clauses? sumti LUhU-clause? free* / KOhA-clause free* / LA-clause free* relative-clauses? CMENE-clause+ free* / (LA-clause / LE-clause) free* sumti-tail KU-clause? free* / li-clause
    public class Sumti_6 : MetaNode { }

    // li-clause <- LI-clause free* mex LOhO-clause? free*
    public class Li_clause : MetaNode { }

    // sumti-tail <- (sumti-6 relative-clauses?)? sumti-tail-1 / relative-clauses sumti-tail-1
    public class Sumti_tail : MetaNode { }

    // sumti-tail-1 <- selbri relative-clauses? / quantifier selbri relative-clauses? / quantifier sumti
    public class Sumti_tail_1 : MetaNode { }

    // relative-clauses <- relative-clause (ZIhE-clause free* relative-clause)*
    public class Relative_clauses : MetaNode { }

    // #; relative-clause <- GOI-clause free* term GEhU-clause? free* / NOI-clause free* subsentence KUhO-clause? free*

    // relative-clause <- relative-clause-sa* relative-clause-1
    public class Relative_clause : MetaNode { }

    // relative-clause-sa <- relative-clause-start (!relative-clause-start (sa-word / SA-clause !relative-clause-start ) )* SA-clause &relative-clause-1
    public class Relative_clause_sa : MetaNode { }

    // relative-clause-1 <- GOI-clause free* term GEhU-clause? free* / NOI-clause free* subsentence KUhO-clause? free*
    public class Relative_clause_1 : MetaNode { }

    // relative-clause-start <- GOI-clause / NOI-clause
    public class Relative_clause_start : MetaNode { }

    // selbri <- tag? selbri-1
    public class Selbri : MetaNode { }

    // selbri-1 <- selbri-2 / NA-clause free* selbri
    public class Selbri_1 : MetaNode { }

    // selbri-2 <- selbri-3 (CO-clause free* selbri-2)?
    public class Selbri_2 : MetaNode { }

    // selbri-3 <- selbri-4+
    public class Selbri_3 : MetaNode { }

    // selbri-4 <- selbri-5 (joik-jek selbri-5 / joik stag? KE-clause free* selbri-3 KEhE-clause? free*)*
    public class Selbri_4 : MetaNode { }

    // selbri-5 <- selbri-6 ((jek / joik) stag? BO-clause free* selbri-5)?
    public class Selbri_5 : MetaNode { }

    // selbri-6 <- tanru-unit (BO-clause free* selbri-6)? / NAhE-clause? free* guhek selbri gik selbri-6
    public class Selbri_6 : MetaNode { }

    // tanru-unit <- tanru-unit-1 (CEI-clause free* tanru-unit-1)*
    public class Tanru_unit : MetaNode { }

    // tanru-unit-1 <- tanru-unit-2 linkargs?
    public class Tanru_unit_1 : MetaNode { }

    // # ** zei is part of BRIVLA-clause
    // tanru-unit-2 <- BRIVLA-clause free* / GOhA-clause RAhO-clause? free* / KE-clause free* selbri-3 KEhE-clause? free* / ME-clause free* (sumti / lerfu-string) MEhU-clause? free* MOI-clause? free* / (number / lerfu-string) MOI-clause free* / NUhA-clause free* mex-operator / SE-clause free* tanru-unit-2 / JAI-clause free* tag? tanru-unit-2 / NAhE-clause free* tanru-unit-2 / NU-clause NAI-clause? free* (joik-jek NU-clause NAI-clause? free*)* subsentence KEI-clause? free*
    public class Tanru_unit_2 : MetaNode { }

    // #; linkargs <- BE-clause free* term links? BEhO-clause? free*

    // linkargs <- linkargs-sa* linkargs-1
    public class Linkargs : MetaNode { }

    // linkargs-1 <- BE-clause free* term links? BEhO-clause? free*
    public class Linkargs_1 : MetaNode { }

    // linkargs-sa <- linkargs-start (!linkargs-start (sa-word / SA-clause !linkargs-start ) )* SA-clause &linkargs-1
    public class Linkargs_sa : MetaNode { }

    // linkargs-start <- BE-clause
    public class Linkargs_start : MetaNode { }

    // #; links <- BEI-clause free* term links?

    // links <- links-sa* links-1
    public class Links : MetaNode { }

    // links-1 <- BEI-clause free* term links?
    public class Links_1 : MetaNode { }

    // links-sa <- links-start (!links-start (sa-word / SA-clause !links-start ) )* SA-clause &links-1
    public class Links_sa : MetaNode { }

    // links-start <- BEI-clause
    public class Links_start : MetaNode { }

    // quantifier <- number !MOI-clause BOI-clause? free* / VEI-clause free* mex VEhO-clause? free*
    public class Quantifier : MetaNode { }

    // #;mex <- mex-1 (operator mex-1)* / rp-clause

    // mex <- mex-sa* mex-0
    public class Mex : MetaNode { }

    // mex-0 <- mex-1 (operator mex-1)* / rp-clause
    public class Mex_0 : MetaNode { }

    // mex-sa <- mex-start (!mex-start (sa-word / SA-clause !mex-start) )* SA-clause &mex-0
    public class Mex_sa : MetaNode { }

    // mex-start <- FUhA-clause / PEhO-clause / operand-start
    public class Mex_start : MetaNode { }

    // rp-clause <- FUhA-clause free* rp-expression
    public class Rp_clause : MetaNode { }

    // mex-1 <- mex-2 (BIhE-clause free* operator mex-1)?
    public class Mex_1 : MetaNode { }

    // mex-2 <- operand / mex-forethought
    public class Mex_2 : MetaNode { }

    // # This is just to make for clearer parse trees
    // mex-forethought <- PEhO-clause? free* operator fore-operands KUhE-clause? free*
    public class Mex_forethought : MetaNode { }
    // fore-operands <- mex-2+ 
    public class Fore_operands : MetaNode { }

    // #li fu'a reboi ci pi'i voboi mu pi'i su'i reboi ci vu'u su'i du li rexa
    // #rp-expression <- rp-operand rp-operand operator
    // #rp-operand <- operand / rp-expression
    // # AKA (almost; this one allows a single operand; above does not.
    // #rp-expression <- rp-expression rp-expression operator / operand

    // # Right recursive version.
    // rp-expression <- operand rp-expression-tail
    public class Rp_expression  : MetaNode { }
    // rp-expression-tail <- rp-expression operator rp-expression-tail / ()
    public class Rp_expression_tail : MetaNode { }

    // #; operator <- operator-1 (joik-jek operator-1 / joik stag? KE-clause free* operator KEhE-clause? free*)*

    // operator <- operator-sa* operator-0
    public class Operator : MetaNode { }

    // operator-0 <- operator-1 (joik-jek operator-1 / joik stag? KE-clause free* operator KEhE-clause? free*)*
    public class Operator_0 : MetaNode { }

    // operator-sa <- operator-start (!operator-start (sa-word / SA-clause !operator-start) )* SA-clause &operator-0
    public class Operator_sa : MetaNode { }

    // operator-start <- guhek / KE-clause / SE-clause? NAhE-clause / SE-clause? MAhO-clause / SE-clause? VUhU-clause
    public class Operator_start : MetaNode { }

    // operator-1 <- operator-2 / guhek operator-1 gik operator-2 / operator-2 (jek / joik) stag? BO-clause free* operator-1
    public class Operator_1 : MetaNode { }

    // operator-2 <- mex-operator / KE-clause free* operator KEhE-clause? free*
    public class Operator_2 : MetaNode { }

    // mex-operator <- SE-clause free* mex-operator / NAhE-clause free* mex-operator / MAhO-clause free* mex TEhU-clause? free* / NAhU-clause free* selbri TEhU-clause? free* / VUhU-clause free*
    public class Mex_operator : MetaNode { }

    // #; operand <- operand-1 (joik-ek stag? KE-clause free* operand KEhE-clause? free*)?

    // operand <- operand-sa* operand-0
    public class Operand : MetaNode { }

    // operand-0 <- operand-1 (joik-ek stag? KE-clause free* operand KEhE-clause? free*)?
    public class Operand_0 : MetaNode { }

    // operand-sa <- operand-start (!operand-start (sa-word / SA-clause !operand-start) )* SA-clause &operand-0
    public class Operand_sa : MetaNode { }

    // operand-start <- quantifier / lerfu-word / NIhE-clause / MOhE-clause / JOhI-clause / gek / LAhE-clause / NAhE-clause
    public class Operand_start : MetaNode { }

    // operand-1 <- operand-2 (joik-ek operand-2)*
    public class Operand_1 : MetaNode { }

    // operand-2 <- operand-3 (joik-ek stag? BO-clause free* operand-2)?
    public class Operand_2 : MetaNode { }

    // operand-3 <- quantifier / lerfu-string !MOI-clause BOI-clause? free* / NIhE-clause free* selbri TEhU-clause? free* / MOhE-clause free* sumti TEhU-clause? free* / JOhI-clause free* mex-2+ TEhU-clause? free* / gek operand gik operand-3 / (LAhE-clause free* / NAhE-clause BO-clause free*) operand LUhU-clause? free*
    public class Operand_3 : MetaNode { }

    // number <- PA-clause (PA-clause / lerfu-word)*
    public class Number : MetaNode { }

    // lerfu-string <- lerfu-word (PA-clause / lerfu-word)*
    public class Lerfu_string : MetaNode { }

    // # ** BU clauses are part of BY-clause
    // lerfu-word <- BY-clause / LAU-clause lerfu-word / TEI-clause lerfu-string FOI-clause
    public class Lerfu_word : MetaNode { }

    // ek <- NA-clause? SE-clause? A-clause NAI-clause?
    public class Ek : MetaNode { }

    // #; gihek <- NA-clause? SE-clause? GIhA-clause NAI-clause?
    // gihek <- gihek-sa* gihek-1
    public class gihek : MetaNode { }

    // gihek-1 <- NA-clause? SE-clause? GIhA-clause NAI-clause?
    public class gihek_1 : MetaNode { }

    // gihek-sa <- gihek-1 (!gihek-1 (sa-word / SA-clause !gihek-1 ) )* SA-clause &gihek
    public class gihek_sa : MetaNode { }

    // jek <- NA-clause? SE-clause? JA-clause NAI-clause?
    public class Jek : MetaNode { }

    // joik <- SE-clause? JOI-clause NAI-clause? / interval / GAhO-clause interval GAhO-clause
    public class Joik : MetaNode { }

    // interval <- SE-clause? BIhI-clause NAI-clause?
    public class Interval : MetaNode { }

    // #; joik-ek <- joik free* / ek free*
    // joik-ek <- joik-ek-sa* joik-ek-1
    public class Joik_ek : MetaNode { }

    // joik-ek-1 <- (joik free* / ek free*)
    public class Joik_ek_1 : MetaNode { }

    // joik-ek-sa <- joik-ek-1 (!joik-ek-1 (sa-word / SA-clause !joik-ek-1 ) )* SA-clause &joik-ek
    public class Joik_ek_sa : MetaNode { }

    // joik-jek <- joik free* / jek free*
    public class Joik_jek : MetaNode { }

    // gek <- SE-clause? GA-clause NAI-clause? free* / joik GI-clause free* / stag gik
    public class Gek : MetaNode { }

    // guhek <- SE-clause? GUhA-clause NAI-clause? free*
    public class Guhek : MetaNode { }

    // gik <- GI-clause NAI-clause? free*
    public class Gik : MetaNode { }

    // tag <- tense-modal (joik-jek tense-modal)*
    public class Tag : MetaNode { }

    // #stag <- simple-tense-modal ((jek / joik) simple-tense-modal)*
    // stag <- simple-tense-modal ((jek / joik) simple-tense-modal)* / tense-modal (joik-jek tense-modal)*
    public class Stag : MetaNode { }

    // tense-modal <- simple-tense-modal free* / FIhO-clause free* selbri FEhU-clause? free*
    public class Tense_modal : MetaNode { }

    // simple-tense-modal <- NAhE-clause? SE-clause? BAI-clause NAI-clause? KI-clause? / NAhE-clause? ( ((time space? / space time?) CAhA-clause) / (time space? / space time?) / CAhA-clause ) KI-clause? / KI-clause / CUhE-clause
    public class Simple_tense_modal  : MetaNode { }

    // time <- ZI-clause time-offset* (ZEhA-clause (PU-clause NAI-clause?)?)? interval-property* / ZI-clause? time-offset+ (ZEhA-clause (PU-clause NAI-clause?)?)? interval-property* / ZI-clause? time-offset* ZEhA-clause (PU-clause NAI-clause?)? interval-property* / ZI-clause? time-offset* (ZEhA-clause (PU-clause NAI-clause?)?)? interval-property+
    public class Time : MetaNode { }

    // time-offset <- PU-clause NAI-clause? ZI-clause?
    public class Time_offset : MetaNode { }

    // space <- VA-clause space-offset* space-interval? (MOhI-clause space-offset)? / VA-clause? space-offset+ space-interval? (MOhI-clause space-offset)? / VA-clause? space-offset* space-interval (MOhI-clause space-offset)? / VA-clause? space-offset* space-interval? MOhI-clause space-offset
    public class Space : MetaNode { }

    // space-offset <- FAhA-clause NAI-clause? VA-clause?
    public class Space_offset : MetaNode { }

    // space-interval <- (VEhA-clause / VIhA-clause / VEhA-clause VIhA-clause) (FAhA-clause NAI-clause?)? space-int-props / (VEhA-clause / VIhA-clause / VEhA-clause VIhA-clause) (FAhA-clause NAI-clause?)? / space-int-props
    public class Space_interval : MetaNode { }

    // space-int-props <- (FEhE-clause interval-property)+
    public class Space_int_props : MetaNode { }

    // interval-property <- number ROI-clause NAI-clause? / TAhE-clause NAI-clause? / ZAhO-clause NAI-clause?
    public class Interval_property : MetaNode { }

    // free <- SEI-clause free* (terms CU-clause? free*)? selbri SEhU-clause? / SOI-clause free* sumti sumti? SEhU-clause? / vocative relative-clauses? selbri relative-clauses? DOhU-clause? / vocative relative-clauses? CMENE-clause+ free* relative-clauses? DOhU-clause? / vocative sumti? DOhU-clause? / (number / lerfu-string) MAI-clause / TO-clause text TOI-clause? / xi-clause
    public class Free : MetaNode { }

    // xi-clause <- XI-clause free* (number / lerfu-string) BOI-clause? / XI-clause free* VEI-clause free* mex VEhO-clause?
    public class Xi_clause : MetaNode { }

    // vocative <- (COI-clause NAI-clause?)+ DOI-clause / (COI-clause NAI-clause?) (COI-clause NAI-clause?)* / DOI-clause
    public class Vocative : MetaNode { }

    // indicators <- FUhE-clause? indicator+
    public class Indicators : MetaNode { }

    // indicator <-  ((UI-clause / CAI-clause) NAI-clause? / DAhO-clause / FUhO-clause) !BU-clause
    public class Indicator : MetaNode { }

    public class NAI_clause : MetaNode { }
    public class EOF : MetaNode { }
    public class Spaces : MetaNode { }
    public class Su_clause : MetaNode { }
    public class CMENE_clause : MetaNode { }
    public class Si_clause : MetaNode { }
    public class SI_clause : MetaNode { }
    public class FAhO_clause : MetaNode { }
    public class Dot_star : MetaNode { }
    public class I_clause : MetaNode { }
    public class BO_clause : MetaNode { }
    public class NIhO_clause : MetaNode { }

    public class SpaceChar1 : MetaNode
    {
    }

    public static class Lojban
    {
        public static readonly Parser<char> Comma = Parse.Char(',');
        public static readonly Parser<char> EOF = from main in Parse.Char(',')
                                                  from negative_look_behind in Parse.Not(Parse.AnyChar)
                                                  select main;
        public static readonly Parser<char> SpaceChar1 = Parse.Char('\t');
        public static readonly Parser<char> SpaceChar2 = Parse.Char('\n');
        public static readonly Parser<char> SpaceChar = Parse.Chars(new char[] { '.', '!', '?', ' ', }).Or(SpaceChar1.Or(SpaceChar2));
    }

    public static class ConfigParser
    {

        public static readonly Parser<string> Key = Parse.Letter.AtLeastOnce().Text();
        public static readonly Parser<int> Value = Parse.Decimal.Select(n => Convert.ToInt32(n));
        public static readonly Parser<Property> Property = from key in Key.Token()
                                                           from asign in Parse.Char('=').Token()
                                                           from value in Value.Token()
                                                           select new Property(key, value);
        public static readonly Parser<IEnumerable<Property>> Properties = Property.Many().End();
    }

    class Program
    {

        static void Main(string[] args)
        {
            Parser<IEnumerable<char>> test = Parse.AnyChar.Many();
            //var src = "speed = 300\ntest = 200";
            //var p = ConfigParser.Properties.Parse(src);
            //foreach (var item in p)
            //{
            //    Console.WriteLine(item.Key + "=" + item.Value);
            //}

            var input = "^cat";

            // Sparache
            Parser<string> parser = from look_ahead in Parse.String("^")
                                    from main in Parse.AnyChar.Many().Text()
                                    select main;
            string result = parser.Parse(input);

            Console.WriteLine("Sprache: " + result + " (" + result + ")");

            // 正規表現
            var match = Regex.Match(input, @"(?<!\^).*");
            result = match.Value;
            Console.WriteLine("Regex: " + result + " (" + result + ")");

        }
    }
}
