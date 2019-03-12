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
    public class Rp_expression : MetaNode { }
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
    public class Simple_tense_modal : MetaNode { }

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



    // # ****************
    // # Magic Words
    // # ****************

    // zei-clause <- pre-clause zei-clause-no-pre
    public class Zei_clause : MetaNode { }
    // zei-clause-no-pre <- pre-zei-bu (zei-tail? bu-tail)* zei-tail post-clause
    public class Zei_clause_no_pre : MetaNode { }
    // zei-clause-no-SA <- pre-zei-bu-no-SA (zei-tail? bu-tail)* zei-tail
    public class Zei_clause_no_SA : MetaNode { }

    // bu-clause <- pre-clause bu-clause-no-pre
    public class Be_clause : MetaNode { }
    // bu-clause-no-pre <- pre-zei-bu (bu-tail? zei-tail)* bu-tail post-clause
    public class Be_clause_no_pre : MetaNode { }
    // bu-clause-no-SA <- pre-zei-bu-no-SA (bu-tail? zei-tail)* bu-tail
    public class Be_clause_no_SA : MetaNode { }

    // zei-tail <- (ZEI-clause any-word)+
    public class Zei_tail : MetaNode { }
    // bu-tail <- BU-clause+
    public class Bu_tail : MetaNode { }

    // pre-zei-bu <- (!BU-clause !ZEI-clause !SI-clause !SA-clause !SU-clause !FAhO-clause any-word-SA-handling) si-clause?
    public class Pre_zei_bu : MetaNode { }
    // # LOhU-pre / ZO-pre / ZOI-pre / !ZEI-clause !BU-clause !FAhO-clause !SI-clause !SA-clause !SU-clause any-word-SA-handling si-clause?
    // pre-zei-bu-no-SA <- LOhU-pre / ZO-pre / ZOI-pre / !ZEI-clause !BU-clause !FAhO-clause !SI-clause !SA-clause !SU-clause any-word si-clause?
    public class Pre_zei_bu_no_SA : MetaNode { }

    // dot-star <- .*
    public class Dot_star : MetaNode { }

    // # -- General Morphology Issues
    // #
    // # 1.  Spaces (including '.y') and UI are eaten *after* a word.
    // #
    // # 3.  BAhE is eaten *before* a word.

    // # Handling of what can go after a cmavo
    // post-clause <- spaces? si-clause? !ZEI-clause !BU-clause indicators*
    public class Post_clause : MetaNode { }

    // pre-clause <- BAhE-clause?
    public class Pre_clause : MetaNode { }

    // #any-word-SA-handling <- BRIVLA-pre / known-cmavo-SA / !known-cmavo-pre CMAVO-pre / CMENE-pre
    // any-word-SA-handling <- BRIVLA-pre / known-cmavo-SA / CMAVO-pre / CMENE-pre
    public class Any_word_SA_handling : MetaNode { }

    // known-cmavo-SA <- A-pre / BAI-pre / BAhE-pre / BE-pre / BEI-pre / BEhO-pre / BIhE-pre / BIhI-pre / BO-pre / BOI-pre / BU-pre / BY-pre / CAI-pre / CAhA-pre / CEI-pre / CEhE-pre / CO-pre / COI-pre / CU-pre / CUhE-pre / DAhO-pre / DOI-pre / DOhU-pre / FA-pre / FAhA-pre / FEhE-pre / FEhU-pre / FIhO-pre / FOI-pre / FUhA-pre / FUhE-pre / FUhO-pre / GA-pre / GAhO-pre / GEhU-pre / GI-pre / GIhA-pre / GOI-pre / GOhA-pre / GUhA-pre / I-pre / JA-pre / JAI-pre / JOI-pre / JOhI-pre / KE-pre / KEI-pre / KEhE-pre / KI-pre / KOhA-pre / KU-pre / KUhE-pre / KUhO-pre / LA-pre / LAU-pre / LAhE-pre / LE-pre / LEhU-pre / LI-pre / LIhU-pre / LOhO-pre / LOhU-pre / LU-pre / LUhU-pre / MAI-pre / MAhO-pre / ME-pre / MEhU-pre / MOI-pre / MOhE-pre / MOhI-pre / NA-pre / NAI-pre / NAhE-pre / NAhU-pre / NIhE-pre / NIhO-pre / NOI-pre / NU-pre / NUhA-pre / NUhI-pre / NUhU-pre / PA-pre / PEhE-pre / PEhO-pre / PU-pre / RAhO-pre / ROI-pre / SA-pre / SE-pre / SEI-pre / SEhU-pre / SI-clause / SOI-pre / SU-pre / TAhE-pre / TEI-pre / TEhU-pre / TO-pre / TOI-pre / TUhE-pre / TUhU-pre / UI-pre / VA-pre / VAU-pre / VEI-pre / VEhA-pre / VEhO-pre / VIhA-pre / VUhO-pre / VUhU-pre / XI-pre / ZAhO-pre / ZEI-pre / ZEhA-pre / ZI-pre / ZIhE-pre / ZO-pre / ZOI-pre / ZOhU-pre
    public class Known_cmavo_SA : MetaNode { }

    // # Handling of spaces and things like spaces.
    // # --- SPACE ---
    // # Do *NOT* delete the line above!

    // # SU clauses
    // su-clause <- (erasable-clause / su-word)* SU-clause
    public class Su_clause : MetaNode { }

    // # Handling of SI and interactions with zo and lo'u...le'u

    // si-clause <- ((erasable-clause / si-word / SA-clause) si-clause? SI-clause)+
    public class Si_clause : MetaNode { }

    // erasable-clause <- bu-clause-no-pre !ZEI-clause !BU-clause / zei-clause-no-pre !ZEI-clause !BU-clause
    public class Erasable_clause : MetaNode { }

    // sa-word <- pre-zei-bu
    public class Sa_word : MetaNode { }

    // si-word <- pre-zei-bu
    public class Si_word : MetaNode { }

    // su-word <- !NIhO-clause !LU-clause !TUhE-clause !TO-clause !SU-clause !FAhO-clause any-word-SA-handling
    public class Su_word : MetaNode { }

    // # --- SELMAHO ---
    // # Do *NOT* delete the line above!

    // BRIVLA-clause <- BRIVLA-pre BRIVLA-post / zei-clause
    public class BRIVLA_clause : MetaNode { }
    // BRIVLA-pre <- pre-clause BRIVLA spaces?
    public class BRIVLA_pre : MetaNode { }
    // BRIVLA-post <- post-clause
    public class BRIVLA_post : MetaNode { }
    // BRIVLA-no-SA-handling <- pre-clause BRIVLA post-clause / zei-clause-no-SA
    public class BRIVLA_no_SA_handing : MetaNode { }

    // CMENE-clause <- CMENE-pre CMENE-post
    public class CMENE_clause : MetaNode { }
    // CMENE-pre <- pre-clause CMENE spaces?
    public class CMENE_pre : MetaNode { }
    // CMENE-post <- post-clause
    public class CMENE_post : MetaNode { }
    // CMENE-no-SA-handling <- pre-clause CMENE post-clause
    public class CMENE_no_SA_handing : MetaNode { }

    // CMAVO-clause <- CMAVO-pre CMAVO-post
    public class CMAVO_clause : MetaNode { }
    // CMAVO-pre <- pre-clause CMAVO spaces?
    public class CMAVO_pre : MetaNode { }
    // CMAVO-post <- post-clause
    public class CMAVO_post : MetaNode { }
    // CMAVO-no-SA-handling <- pre-clause CMAVO post-clause
    public class CMAVO_no_SA_handing : MetaNode { }

    // #         eks; basic afterthought logical connectives 
    // A-clause <- A-pre A-post
    public class A_clause : MetaNode { }
    // A-pre <- pre-clause A spaces?
    public class A_pre : MetaNode { }
    // A-post <- post-clause
    public class A_post : MetaNode { }
    // A-no-SA-handling <- pre-clause A post-clause
    public class A_no_SA_handing : MetaNode { }


    // #         modal operators 
    // BAI-clause <- BAI-pre BAI-post
    public class BAI_clause : MetaNode { }
    // BAI-pre <- pre-clause BAI spaces?
    public class BAI_pre : MetaNode { }
    // BAI-post <- post-clause
    public class BAI_post : MetaNode { }
    // BAI-no-SA-handling <- pre-clause BAI post-clause
    public class BAI_no_SA_handing : MetaNode { }

    // #         next word intensifier 
    // BAhE-clause <- (BAhE-pre BAhE-post)+
    public class BAhE_clause : MetaNode { }
    // BAhE-pre <- BAhE spaces?
    public class BAhE_pre : MetaNode { }
    // BAhE-post <- si-clause? !ZEI-clause !BU-clause
    public class BAhE_post : MetaNode { }
    // BAhE-no-SA-handling <- BAhE spaces? BAhE-post
    public class BAhE_no_SA_handing : MetaNode { }

    // #         sumti link to attach sumti to a selbri 
    // BE-clause <- BE-pre BE-post
    public class BE_clause : MetaNode { }
    // BE-pre <- pre-clause BE spaces?
    public class BE_pre : MetaNode { }
    // BE-post <- post-clause
    public class BE_post : MetaNode { }
    // BE-no-SA-handling <- pre-clause BE post-clause
    public class BE_no_SA_handing : MetaNode { }

    // #         multiple sumti separator between BE, BEI 
    // BEI-clause <- BEI-pre BEI-post
    public class BEI_clause : MetaNode { }
    // BEI-pre <- pre-clause BEI spaces?
    public class BEI_pre : MetaNode { }
    // BEI-post <- post-clause
    public class BEI_post : MetaNode { }
    // BEI-no-SA-handling <- pre-clause BEI post-clause
    public class BEI_no_SA_handing : MetaNode { }

    // #         terminates BEBEI specified descriptors 
    // BEhO-clause <- BEhO-pre BEhO-post
    public class BEhO_clause : MetaNode { }
    // BEhO-pre <- pre-clause BEhO spaces?
    public class BEhO_pre : MetaNode { }
    // BEhO-post <- post-clause
    public class BEhO_post : MetaNode { }
    // BEhO-no-SA-handling <- pre-clause BEhO post-clause
    public class BEhO_no_SA_handing : MetaNode { }

    // #         prefix for high-priority MEX operator 
    // BIhE-clause <- BIhE-pre BIhE-post
    public class BIhE_clause : MetaNode { }
    // BIhE-pre <- pre-clause BIhE spaces?
    public class BIhE_pre : MetaNode { }
    // BIhE-post <- post-clause
    public class BIhE_post : MetaNode { }
    // BIhE-no-SA-handling <- pre-clause BIhE post-clause
    public class BIhE_no_SA_handing : MetaNode { }

    // #         interval component of JOI 
    // BIhI-clause <- BIhI-pre BIhI-post
    public class BIhI_clause : MetaNode { }
    // BIhI-pre <- pre-clause BIhI spaces?
    public class BIhI_pre : MetaNode { }
    // BIhI-post <- post-clause
    public class BIhI_post : MetaNode { }
    // BIhI-no-SA-handling <- pre-clause BIhI post-clause
    public class BIhI_no_SA_handing : MetaNode { }

    // #         joins two units with shortest scope 
    // BO-clause <- BO-pre BO-post
    public class BO_clause : MetaNode { }
    // BO-pre <- pre-clause BO spaces?
    public class BO_pre : MetaNode { }
    // BO-post <- post-clause
    public class BO_post : MetaNode { }
    // BO-no-SA-handling <- pre-clause BO post-clause
    public class BO_no_SA_handing : MetaNode { }

    // #         number or lerfu-string terminator 
    // BOI-clause <- BOI-pre BOI-post
    public class BOI_clause : MetaNode { }
    // BOI-pre <- pre-clause BOI spaces?
    public class BOI_pre : MetaNode { }
    // BOI-post <- post-clause
    public class BOI_post : MetaNode { }
    // BOI-no-SA-handling <- pre-clause BOI post-clause
    public class BOI_no_SA_handing : MetaNode { }

    // #         turns any word into a BY lerfu word 
    // BU-clause <- BU-pre BU-post
    public class BU_clause : MetaNode { }
    // BU-clause-no-SA <- BU-pre-no-SA BU BU-post
    // BU-pre <- pre-clause BU spaces?
    public class BU_pre : MetaNode { }
    // BU-pre-no-SA <- pre-clause
    // BU-post <- spaces?
    public class BU_post : MetaNode { }
    // BU-no-SA-handling <- pre-clause BU spaces?
    public class BU_no_SA_handing : MetaNode { }

    // #         individual lerfu words 
    // BY-clause <- BY-pre BY-post / bu-clause
    public class BY_clause : MetaNode { }
    // BY-pre <- pre-clause BY spaces?
    public class BY_pre : MetaNode { }
    // BY-post <- post-clause
    public class BY_post : MetaNode { }
    // BY-no-SA-handling <- pre-clause BY post-clause / bu-clause-no-SA
    public class BY_no_SA_handing : MetaNode { }


    // #         specifies actualitypotentiality of tense 
    // CAhA-clause <- CAhA-pre CAhA-post
    public class CAhA_clause : MetaNode { }
    // CAhA-pre <- pre-clause CAhA spaces?
    public class CAhA_pre : MetaNode { }
    // CAhA-post <- post-clause
    public class CAhA_post : MetaNode { }
    // CAhA-no-SA-handling <- pre-clause CAhA post-clause
    public class CAhA_no_SA_handing : MetaNode { }

    // #         afterthought intensity marker 
    // CAI-clause <- CAI-pre CAI-post
    public class CAI_clause : MetaNode { }
    // CAI-pre <- pre-clause CAI spaces?
    public class CAI_pre : MetaNode { }
    // CAI-post <- post-clause
    public class CAI_post : MetaNode { }
    // CAI-no-SA-handling <- pre-clause CAI post-clause
    public class CAI_no_SA_handing : MetaNode { }

    // #         pro-bridi assignment operator 
    // CEI-clause <- CEI-pre CEI-post
    public class CEI_clause : MetaNode { }
    // CEI-pre <- pre-clause CEI spaces?
    public class CEI_pre : MetaNode { }
    // CEI-post <- post-clause
    public class CEI_post : MetaNode { }
    // CEI-no-SA-handling <- pre-clause CEI post-clause
    public class CEI_no_SA_handing : MetaNode { }

    // #         afterthought term list connective 
    // CEhE-clause <- CEhE-pre CEhE-post
    public class CEhE_clause : MetaNode { }
    // CEhE-pre <- pre-clause CEhE spaces?
    public class CEhE_pre : MetaNode { }
    // CEhE-post <- post-clause
    public class CEhE_post : MetaNode { }
    // CEhE-no-SA-handling <- pre-clause CEhE post-clause
    public class CEhE_no_SA_handing : MetaNode { }

    // #         names; require consonant end, then pause no

    // #                                    LA or DOI selma'o embedded, pause before if

    // #                                    vowel initial and preceded by a vowel 

    // #         tanru inversion  
    // CO-clause <- CO-pre CO-post
    public class CO_clause : MetaNode { }
    // CO-pre <- pre-clause CO spaces?
    public class CO_pre : MetaNode { }
    // CO-post <- post-clause
    public class CO_post : MetaNode { }
    // CO-no-SA-handling <- pre-clause CO post-clause
    public class CO_no_SA_handing : MetaNode { }
    // COI-clause <- COI-pre COI-post
    public class COI_clause : MetaNode { }
    // COI-pre <- pre-clause COI spaces?
    public class COI_pre : MetaNode { }
    // COI-post <- post-clause
    public class COI_post : MetaNode { }
    // COI-no-SA-handling <- pre-clause COI post-clause
    public class COI_no_SA_handing : MetaNode { }

    // #         vocative marker permitted inside names; must

    // #                                    always be followed by pause or DOI 

    // #         separator between head sumti and selbri 
    // CU-clause <- CU-pre CU-post
    public class CU_clause : MetaNode { }
    // CU-pre <- pre-clause CU spaces?
    public class CU_pre : MetaNode { }
    // CU-post <- post-clause
    public class CU_post : MetaNode { }
    // CU-no-SA-handling <- pre-clause CU post-clause
    public class CU_no_SA_handing : MetaNode { }

    // #         tensemodal question 
    // CUhE-clause <- CUhE-pre CUhE-post
    public class CUhE_clause : MetaNode { }
    // CUhE-pre <- pre-clause CUhE spaces?
    public class CUhE_pre : MetaNode { }
    // CUhE-post <- post-clause
    public class CUhE_post : MetaNode { }
    // CUhE-no-SA-handling <- pre-clause CUhE post-clause
    public class CUhE_no_SA_handing : MetaNode { }


    // #         cancel anaphoracataphora assignments 
    // DAhO-clause <- DAhO-pre DAhO-post
    public class DAhO_clause : MetaNode { }
    // DAhO-pre <- pre-clause DAhO spaces?
    public class DAhO_pre : MetaNode { }
    // DAhO-post <- post-clause
    public class DAhO_post : MetaNode { }
    // DAhO-no-SA-handling <- pre-clause DAhO post-clause
    public class DAhO_no_SA_handing : MetaNode { }

    // #         vocative marker 
    // DOI-clause <- DOI-pre DOI-post
    public class DOI_clause : MetaNode { }
    // DOI-pre <- pre-clause DOI spaces?
    public class DOI_pre : MetaNode { }
    // DOI-post <- post-clause
    public class DOI_post : MetaNode { }
    // DOI-no-SA-handling <- pre-clause DOI post-clause
    public class DOI_no_SA_handing : MetaNode { }

    // #         terminator for DOI-marked vocatives 
    // DOhU-clause <- DOhU-pre DOhU-post
    public class DOhU_clause : MetaNode { }
    // DOhU-pre <- pre-clause DOhU spaces?
    public class DOhU_pre : MetaNode { }
    // DOhU-post <- post-clause
    public class DOhU_post : MetaNode { }
    // DOhU-no-SA-handling <- pre-clause DOhU post-clause
    public class DOhU_no_SA_handing : MetaNode { }


    // #         modifier head generic case tag 
    // FA-clause <- FA-pre FA-post
    public class FA_clause : MetaNode { }
    // FA-pre <- pre-clause FA spaces?
    public class FA_pre : MetaNode { }
    // FA-post <- post-clause
    public class FA_post : MetaNode { }
    // FA-no-SA-handling <- pre-clause FA post-clause
    public class FA_no_SA_handing : MetaNode { }

    // #         superdirections in space 
    // FAhA-clause <- FAhA-pre FAhA-post
    public class FAhA_clause : MetaNode { }
    // FAhA-pre <- pre-clause FAhA spaces?
    public class FAhA_pre : MetaNode { }
    // FAhA-post <- post-clause
    public class FAhA_post : MetaNode { }
    // FAhA-no-SA-handling <- pre-clause FAhA post-clause
    public class FAhA_no_SA_handing : MetaNode { }


    // #         normally elided 'done pause' to indicate end
    // #                                    of utterance string 

    // FAhO-clause <- pre-clause FAhO spaces?
    public class FAhO_clause : MetaNode { }

    // #         space interval mod flag 
    // FEhE-clause <- FEhE-pre FEhE-post
    public class FEhE_clause : MetaNode { }
    // FEhE-pre <- pre-clause FEhE spaces?
    public class FEhE_pre : MetaNode { }
    // FEhE-post <- post-clause
    public class FEhE_post : MetaNode { }
    // FEhE-no-SA-handling <- pre-clause FEhE post-clause
    public class FEhE_no_SA_handing : MetaNode { }

    // #         ends bridi to modal conversion 
    // FEhU-clause <- FEhU-pre FEhU-post
    public class FEhU_clause : MetaNode { }
    // FEhU-pre <- pre-clause FEhU spaces?
    public class FEhU_pre : MetaNode { }
    // FEhU-post <- post-clause
    public class FEhU_post : MetaNode { }
    // FEhU-no-SA-handling <- pre-clause FEhU post-clause
    public class FEhU_no_SA_handing : MetaNode { }

    // #         marks bridi to modal conversion 
    // FIhO-clause <- FIhO-pre FIhO-post
    public class FIhO_clause : MetaNode { }
    // FIhO-pre <- pre-clause FIhO spaces?
    public class FIhO_pre : MetaNode { }
    // FIhO-post <- post-clause
    public class FIhO_post : MetaNode { }
    // FIhO-no-SA-handling <- pre-clause FIhO post-clause
    public class FIhO_no_SA_handing : MetaNode { }

    // #         end compound lerfu 
    // FOI-clause <- FOI-pre FOI-post
    public class FOI_clause : MetaNode { }
    // FOI-pre <- pre-clause FOI spaces?
    public class FOI_pre : MetaNode { }
    // FOI-post <- post-clause
    public class FOI_post : MetaNode { }
    // FOI-no-SA-handling <- pre-clause FOI post-clause
    public class FOI_no_SA_handing : MetaNode { }

    // #         reverse Polish flag 
    // FUhA-clause <- FUhA-pre FUhA-post
    public class FuhA_clause : MetaNode { }
    // FUhA-pre <- pre-clause FUhA spaces?
    public class FuhA_pre : MetaNode { }
    // FUhA-post <- post-clause
    public class FuhA_post : MetaNode { }
    // FUhA-no-SA-handling <- pre-clause FUhA post-clause
    public class FuhA_no_SA_handing : MetaNode { }

    // #         open long scope for indicator 
    // FUhE-clause <- FUhE-pre FUhE-post
    public class FUhE_clause : MetaNode { }
    // FUhE-pre <- pre-clause FUhE spaces?
    public class FUhE_pre : MetaNode { }
    // FUhE-post <- !BU-clause spaces? !ZEI-clause !BU-clause
    public class FUhE_post : MetaNode { }
    // FUhE-no-SA-handling <- pre-clause FUhE post-clause
    public class FUhE_no_SA_handing : MetaNode { }

    // #         close long scope for indicator 
    // FUhO-clause <- FUhO-pre FUhO-post
    public class FUhO_clause : MetaNode { }
    // FUhO-pre <- pre-clause FUhO spaces?
    public class FUhO_pre : MetaNode { }
    // FUhO-post <- post-clause
    public class FUhO_post : MetaNode { }
    // FUhO-no-SA-handling <- pre-clause FUhO post-clause
    public class FUhO_no_SA_handing : MetaNode { }


    // #         geks; forethought logical connectives 
    // GA-clause <- GA-pre GA-post
    public class GA_clause : MetaNode { }
    // GA-pre <- pre-clause GA spaces?
    public class GA_pre : MetaNode { }
    // GA-post <- post-clause
    public class GA_post : MetaNode { }
    // GA-no-SA-handling <- pre-clause GA post-clause
    public class GA_no_SA_handing : MetaNode { }

    // #         openclosed interval markers for BIhI 
    // GAhO-clause <- GAhO-pre GAhO-post
    public class GAhO_clause : MetaNode { }
    // GAhO-pre <- pre-clause GAhO spaces?
    public class GAhO_pre : MetaNode { }
    // GAhO-post <- post-clause
    public class GAhO_post : MetaNode { }
    // GAhO-no-SA-handling <- pre-clause GAhO post-clause
    public class GAhO_no_SA_handing : MetaNode { }

    // #         marker ending GOI relative clauses 
    // GEhU-clause <- GEhU-pre GEhU-post
    public class GEhU_clause : MetaNode { }
    // GEhU-pre <- pre-clause GEhU spaces?
    public class GEhU_pre : MetaNode { }
    // GEhU-post <- post-clause
    public class GEhU_post : MetaNode { }
    // GEhU-no-SA-handling <- pre-clause GEhU post-clause
    public class GEhU_no_SA_handing : MetaNode { }

    // #         forethought medial marker 
    // GI-clause <- GI-pre GI-post
    public class GI_clause : MetaNode { }
    // GI-pre <- pre-clause GI spaces?
    public class GI_pre : MetaNode { }
    // GI-post <- post-clause
    public class GI_post : MetaNode { }
    // GI-no-SA-handling <- pre-clause GI post-clause
    public class GI_no_SA_handing : MetaNode { }

    // #         logical connectives for bridi-tails 
    // GIhA-clause <- GIhA-pre GIhA-post
    public class GIhA_clause : MetaNode { }
    // GIhA-pre <- pre-clause GIhA spaces?
    public class GIhA_pre : MetaNode { }
    // GIhA-post <- post-clause
    public class GIhA_post : MetaNode { }
    // GIhA-no-SA-handling <- pre-clause GIhA post-clause
    public class GIhA_no_SA_handing : MetaNode { }

    // #         attaches a sumti modifier to a sumti 
    // GOI-clause <- GOI-pre GOI-post
    public class GOI_clause : MetaNode { }
    // GOI-pre <- pre-clause GOI spaces?
    public class GOI_pre : MetaNode { }
    // GOI-post <- post-clause
    public class GOI_post : MetaNode { }
    // GOI-no-SA-handling <- pre-clause GOI post-clause
    public class GOI_no_SA_handing : MetaNode { }

    // #         pro-bridi 
    // GOhA-clause <- GOhA-pre GOhA-post
    public class GOhA_clause : MetaNode { }
    // GOhA-pre <- pre-clause GOhA spaces?
    public class GOhA_pre : MetaNode { }
    // GOhA-post <- post-clause
    public class GOhA_post : MetaNode { }
    // GOhA-no-SA-handling <- pre-clause GOhA post-clause
    public class GOhA_no_SA_handing : MetaNode { }

    // #         GEK for tanru units, corresponds to JEKs 
    // GUhA-clause <- GUhA-pre GUhA-post
    public class GUhA_clause : MetaNode { }
    // GUhA-pre <- pre-clause GUhA spaces?
    public class GUhA_pre : MetaNode { }
    // GUhA-post <- post-clause
    public class GUhA_post : MetaNode { }
    // GUhA-no-SA-handling <- pre-clause GUhA post-clause
    public class GUhA_no_SA_handing : MetaNode { }


    // #         sentence link 
    // I-clause <- sentence-sa* I-pre I-post
    public class I_clause : MetaNode { }
    // I-pre <- pre-clause I spaces?
    public class I_pre : MetaNode { }
    // I-post <- post-clause
    public class I_post : MetaNode { }
    // I-no-SA-handling <- pre-clause I post-clause
    public class I_no_SA_handing : MetaNode { }


    // #         jeks; logical connectives within tanru 
    // JA-clause <- JA-pre JA-post
    public class JA_clause : MetaNode { }
    // JA-pre <- pre-clause JA spaces?
    public class JA_pre : MetaNode { }
    // JA-post <- post-clause
    public class JA_post : MetaNode { }
    // JA-no-SA-handling <- pre-clause JA post-clause
    public class JA_no_SA_handing : MetaNode { }

    // #         modal conversion flag 
    // JAI-clause <- JAI-pre JAI-post
    public class JAI_clause : MetaNode { }
    // JAI-pre <- pre-clause JAI spaces?
    public class JAI_pre : MetaNode { }
    // JAI-post <- post-clause
    public class JAI_post : MetaNode { }
    // JAI-no-SA-handling <- pre-clause JAI post-clause
    public class JAI_no_SA_handing : MetaNode { }

    // #         flags an array operand 
    // JOhI-clause <- JOhI-pre JOhI-post
    public class JOhI_clause : MetaNode { }
    // JOhI-pre <- pre-clause JOhI spaces?
    public class JOhI_pre : MetaNode { }
    // JOhI-post <- post-clause
    public class JOhI_post : MetaNode { }
    // JOhI-no-SA-handling <- pre-clause JOhI post-clause
    public class JOhI_no_SA_handing : MetaNode { }

    // #         non-logical connectives 
    // JOI-clause <- JOI-pre JOI-post
    public class JOI_clause : MetaNode { }
    // JOI-pre <- pre-clause JOI spaces?
    public class JOI_pre : MetaNode { }
    // JOI-post <- post-clause
    public class JOI_post : MetaNode { }
    // JOI-no-SA-handling <- pre-clause JOI post-clause
    public class JOI_no_SA_handing : MetaNode { }


    // #         left long scope marker 
    // KE-clause <- KE-pre KE-post
    public class KE_clause : MetaNode { }
    // KE-pre <- pre-clause KE spaces?
    public class KE_pre : MetaNode { }
    // KE-post <- post-clause
    public class KE_post : MetaNode { }
    // KE-no-SA-handling <- pre-clause KE post-clause
    public class KE_no_SA_handing : MetaNode { }

    // #         right terminator for KE groups 
    // KEhE-clause <- KEhE-pre KEhE-post
    public class KEhE_clause : MetaNode { }
    // KEhE-pre <- pre-clause KEhE spaces?
    public class KEhE_pre : MetaNode { }
    // KEhE-post <- post-clause
    public class KEhE_post : MetaNode { }
    // KEhE-no-SA-handling <- pre-clause KEhE post-clause
    public class KEhE_no_SA_handing : MetaNode { }

    // #         right terminator, NU abstractions 
    // KEI-clause <- KEI-pre KEI-post
    public class KEI_clause : MetaNode { }
    // KEI-pre <- pre-clause KEI spaces?
    public class KEI_pre : MetaNode { }
    // KEI-post <- post-clause
    public class KEI_post : MetaNode { }
    // KEI-no-SA-handling <- pre-clause KEI post-clause
    public class KEI_no_SA_handing : MetaNode { }

    // #         multiple utterance scope for tenses 
    // KI-clause <- KI-pre KI-post
    public class KI_clause : MetaNode { }
    // KI-pre <- pre-clause KI spaces?
    public class KI_pre : MetaNode { }
    // KI-post <- post-clause
    public class KI_post : MetaNode { }
    // KI-no-SA-handling <- pre-clause KI post-clause
    public class KI_no_SA_handing : MetaNode { }

    // #         sumti anaphora 
    // KOhA-clause <- KOhA-pre KOhA-post
    public class KOhA_clause : MetaNode { }
    // KOhA-pre <- pre-clause KOhA spaces?
    public class KOhA_pre : MetaNode { }
    // KOhA-post <- post-clause
    public class KOhA_post : MetaNode { }
    // KOhA-no-SA-handling <- pre-clause KOhA spaces?
    public class KOhA_no_SA_handing : MetaNode { }

    // #         right terminator for descriptions, etc. 
    // KU-clause <- KU-pre KU-post
    public class KU_clause : MetaNode { }
    // KU-pre <- pre-clause KU spaces?
    public class KU_pre : MetaNode { }
    // KU-post <- post-clause
    public class KU_post : MetaNode { }
    // KU-no-SA-handling <- pre-clause KU post-clause
    public class KU_no_SA_handing : MetaNode { }

    // #         MEX forethought delimiter 
    // KUhE-clause <- KUhE-pre KUhE-post
    public class KUhE_clause : MetaNode { }
    // KUhE-pre <- pre-clause KUhE spaces?
    public class KUhE_pre : MetaNode { }
    // KUhE-post <- post-clause
    public class KUhE_post : MetaNode { }
    // KUhE-no-SA-handling <- pre-clause KUhE post-clause
    public class KUhE_no_SA_handing : MetaNode { }

    // #         right terminator, NOI relative clauses 
    // KUhO-clause <- KUhO-pre KUhO-post
    public class KUhO_clause : MetaNode { }
    // KUhO-pre <- pre-clause KUhO spaces?
    public class KUhO_pre : MetaNode { }
    // KUhO-post <- post-clause
    public class KUhO_post : MetaNode { }
    // KUhO-no-SA-handling <- pre-clause KUhO post-clause
    public class KUhO_no_SA_handing : MetaNode { }


    // #         name descriptors 
    // LA-clause <- LA-pre LA-post
    public class LA_clause : MetaNode { }
    // LA-pre <- pre-clause LA spaces?
    public class LA_pre : MetaNode { }
    // LA-post <- post-clause
    public class LA_post : MetaNode { }
    // LA-no-SA-handling <- pre-clause LA post-clause
    public class LA_no_SA_handing : MetaNode { }

    // #         lerfu prefixes 
    // LAU-clause <- LAU-pre LAU-post
    public class LAU_clause : MetaNode { }
    // LAU-pre <- pre-clause LAU spaces?
    public class LAU_pre : MetaNode { }
    // LAU-post <- post-clause
    public class LAU_post : MetaNode { }
    // LAU-no-SA-handling <- pre-clause LAU post-clause
    public class LAU_no_SA_handing : MetaNode { }

    // #         sumti qualifiers 
    // LAhE-clause <- LAhE-pre LAhE-post
    public class LAhE_clause : MetaNode { }
    // LAhE-pre <- pre-clause LAhE spaces?
    public class LAhE_pre : MetaNode { }
    // LAhE-post <- post-clause
    public class LAhE_post : MetaNode { }
    // LAhE-no-SA-handling <- pre-clause LAhE post-clause
    public class LAhE_no_SA_handing : MetaNode { }

    // #         sumti descriptors 
    // LE-clause <- LE-pre LE-post
    public class LE_clause : MetaNode { }
    // LE-pre <- pre-clause LE spaces?
    public class LE_pre : MetaNode { }
    // LE-post <- post-clause
    public class LE_post : MetaNode { }
    // LE-no-SA-handling <- pre-clause LE post-clause
    public class LE_no_SA_handing : MetaNode { }


    // #         possibly ungrammatical text right quote 
    // LEhU-clause <- LEhU-pre LEhU-post
    public class LEhU_clause : MetaNode { }
    // LEhU-pre <- pre-clause LEhU spaces?
    public class LEhU_pre : MetaNode { }
    // LEhU-post <- spaces?
    public class LEhU_post : MetaNode { }
    // LEhU-clause-no-SA <- LEhU-pre-no-SA LEhU-post
    public class LEhU_clause_no_SA : MetaNode { }
    // LEhU-pre-no-SA <- pre-clause LEhU spaces?
    public class LEhU_pre_no_SA : MetaNode { }
    // LEhU-no-SA-handling <- pre-clause LEhU post-clause
    public class LEhU_no_SA_handling : MetaNode { }

    // #         convert number to sumti 
    // LI-clause <- LI-pre LI-post
    public class LI_clause : MetaNode { }
    // LI-pre <- pre-clause LI spaces?
    public class LI_pre : MetaNode { }
    // LI-post <- post-clause
    public class LI_post : MetaNode { }
    // LI-no-SA-handling <- pre-clause LI post-clause
    public class LI_no_SA_handing : MetaNode { }

    // #         grammatical text right quote 
    // LIhU-clause <- LIhU-pre LIhU-post
    public class LIhU_clause : MetaNode { }
    // LIhU-pre <- pre-clause LIhU spaces?
    public class LIhU_pre : MetaNode { }
    // LIhU-post <- post-clause
    public class LIhU_post : MetaNode { }
    // LIhU-no-SA-handling <- pre-clause LIhU post-clause
    public class LIhU_no_SA_handing : MetaNode { }

    // #         elidable terminator for LI 
    // LOhO-clause <- LOhO-pre LOhO-post
    public class LOhO_clause : MetaNode { }
    // LOhO-pre <- pre-clause LOhO spaces?
    public class LOhO_pre : MetaNode { }
    // LOhO-post <- post-clause
    public class LOhO_post : MetaNode { }
    // LOhO-no-SA-handling <- pre-clause LOhO post-clause
    public class LOhO_no_SA_handing : MetaNode { }

    // #         possibly ungrammatical text left quote 
    // LOhU-clause <- LOhU-pre LOhU-post
    public class LOhU_clause : MetaNode { }
    // LOhU-pre <- pre-clause LOhU spaces? (!LEhU any-word)* LEhU-clause spaces?
    public class LOhU_pre : MetaNode { }
    // LOhU-post <- post-clause
    public class LOhU_post : MetaNode { }
    // LOhU-no-SA-handling <- pre-clause LOhU spaces? (!LEhU any-word)* LEhU-clause spaces?
    public class LOhU_no_SA_handing : MetaNode { }

    // #         grammatical text left quote 
    // LU-clause <- LU-pre LU-post
    public class LU_clause : MetaNode { }
    // LU-pre <- pre-clause LU spaces?
    public class LU_pre : MetaNode { }
    // LU-post <- post-clause
    public class LU_post : MetaNode { }
    // LU-no-SA-handling <- pre-clause LU post-clause
    public class LU_no_SA_handing : MetaNode { }

    // #         LAhE close delimiter 
    // LUhU-clause <- LUhU-pre LUhU-post
    public class LUhU_clause : MetaNode { }
    // LUhU-pre <- pre-clause LUhU spaces?
    public class LUhU_pre : MetaNode { }
    // LUhU-post <- post-clause
    public class LUhU_post : MetaNode { }
    // LUhU-no-SA-handling <- pre-clause LUhU post-clause
    public class LUhU_no_SA_handing : MetaNode { }


    // #         change MEX expressions to MEX operators 
    // MAhO-clause <- MAhO-pre MAhO-post
    public class MAhO_clause : MetaNode { }
    // MAhO-pre <- pre-clause MAhO spaces?
    public class MAhO_pre : MetaNode { }
    // MAhO-post <- post-clause
    public class MAhO_post : MetaNode { }
    // MAhO-no-SA-handling <- pre-clause MAhO post-clause
    public class MAhO_no_SA_handing : MetaNode { }

    // #         change numbers to utterance ordinals 
    // MAI-clause <- MAI-pre MAI-post
    public class MAI_clause : MetaNode { }
    // MAI-pre <- pre-clause MAI spaces?
    public class MAI_pre : MetaNode { }
    // MAI-post <- post-clause
    public class MAI_post : MetaNode { }
    // MAI-no-SA-handling <- pre-clause MAI post-clause
    public class MAI_no_SA_handing : MetaNode { }

    // #         converts a sumti into a tanru_unit 
    // ME-clause <- ME-pre ME-post
    public class MA_clause : MetaNode { }
    // ME-pre <- pre-clause ME spaces?
    public class MA_pre : MetaNode { }
    // ME-post <- post-clause
    public class MA_post : MetaNode { }
    // ME-no-SA-handling <- pre-clause ME post-clause
    public class MA_no_SA_handing : MetaNode { }

    // #         terminator for ME 
    // MEhU-clause <- MEhU-pre MEhU-post
    public class MEhU_clause : MetaNode { }
    // MEhU-pre <- pre-clause MEhU spaces?
    public class MEhU_pre : MetaNode { }
    // MEhU-post <- post-clause
    public class MEhU_post : MetaNode { }
    // MEhU-no-SA-handling <- pre-clause MEhU post-clause
    public class MEhU_no_SA_handing : MetaNode { }

    // #         change sumti to operand, inverse of LI 
    // MOhE-clause <- MOhE-pre MOhE-post
    public class MOhE_clause : MetaNode { }
    // MOhE-pre <- pre-clause MOhE spaces?
    public class MOhE_pre : MetaNode { }
    // MOhE-post <- post-clause
    public class MOhE_post : MetaNode { }
    // MOhE-no-SA-handling <- pre-clause MOhE post-clause
    public class MOhE_no_SA_handing : MetaNode { }

    // #         motion tense marker 
    // MOhI-clause <- MOhI-pre MOhI-post
    public class MOhI_clause : MetaNode { }
    // MOhI-pre <- pre-clause MOhI spaces?
    public class MOhI_pre : MetaNode { }
    // MOhI-post <- post-clause
    public class MOhI_post : MetaNode { }
    // MOhI-no-SA-handling <- pre-clause MOhI post-clause
    public class MOhI_no_SA_handing : MetaNode { }

    // #         change number to selbri 
    // MOI-clause <- MOI-pre MOI-post
    public class MOI_clause : MetaNode { }
    // MOI-pre <- pre-clause MOI spaces?
    public class MOI_pre : MetaNode { }
    // MOI-post <- post-clause
    public class MOI_post : MetaNode { }
    // MOI-no-SA-handling <- pre-clause MOI post-clause
    public class MOI_no_SA_handing : MetaNode { }


    // #         bridi negation  
    // NA-clause <- NA-pre NA-post
    public class NA_clause : MetaNode { }
    // NA-pre <- pre-clause NA spaces?
    public class NA_pre : MetaNode { }
    // NA-post <- post-clause
    public class NA_post : MetaNode { }
    // NA-no-SA-handling <- pre-clause NA post-clause
    public class NA_no_SA_handing : MetaNode { }

    // #         attached to words to negate them 
    // NAI-clause <- NAI-pre NAI-post
    public class NAI_clause : MetaNode { }
    // NAI-pre <- pre-clause NAI spaces?
    public class NAI_pre : MetaNode { }
    // NAI-post <- post-clause
    public class NAI_post : MetaNode { }
    // NAI-no-SA-handling <- pre-clause NAI post-clause
    public class NAI_no_SA_handing : MetaNode { }

    // #         scalar negation  
    // NAhE-clause <- NAhE-pre NAhE-post
    public class NAhE_clause : MetaNode { }
    // NAhE-pre <- pre-clause NAhE spaces?
    public class NAhE_pre : MetaNode { }
    // NAhE-post <- post-clause
    public class NAhE_post : MetaNode { }
    // NAhE-no-SA-handling <- pre-clause NAhE post-clause
    public class NAhE_no_SA_handing : MetaNode { }

    // #         change a selbri into an operator 
    // NAhU-clause <- NAhU-pre NAhU-post
    public class NAhU_clause : MetaNode { }
    // NAhU-pre <- pre-clause NAhU spaces?
    public class NAhU_pre : MetaNode { }
    // NAhU-post <- post-clause
    public class NAhU_post : MetaNode { }
    // NAhU-no-SA-handling <- pre-clause NAhU post-clause
    public class NAhU_no_SA_handing : MetaNode { }

    // #         change selbri to operand; inverse of MOI 
    // NIhE-clause <- NIhE-pre NIhE-post
    public class NIhE_clause : MetaNode { }
    // NIhE-pre <- pre-clause NIhE spaces?
    public class NIhE_pre : MetaNode { }
    // NIhE-post <- post-clause
    public class NIhE_post : MetaNode { }
    // NIhE-no-SA-handling <- pre-clause NIhE post-clause
    public class NIhE_no_SA_handing : MetaNode { }

    // #         new paragraph; change of subject 
    // NIhO-clause <- sentence-sa* NIhO-pre NIhO-post
    public class NIhO_clause : MetaNode { }
    // NIhO-pre <- pre-clause NIhO spaces?
    public class NIhO_pre : MetaNode { }
    // NIhO-post <- su-clause* post-clause
    public class NIhO_post : MetaNode { }
    // NIhO-no-SA-handling <- pre-clause NIhO su-clause* post-clause
    public class NIhO_no_SA_handing : MetaNode { }

    // #         attaches a subordinate clause to a sumti 
    // NOI-clause <- NOI-pre NOI-post
    public class NOI_clause : MetaNode { }
    // NOI-pre <- pre-clause NOI spaces?
    public class NOI_pre : MetaNode { }
    // NOI-post <- post-clause
    public class NOI_post : MetaNode { }
    // NOI-no-SA-handling <- pre-clause NOI post-clause
    public class NOI_no_SA_handing : MetaNode { }

    // #         abstraction  
    // NU-clause <- NU-pre NU-post
    public class NU_clause : MetaNode { }
    // NU-pre <- pre-clause NU spaces?
    public class NU_pre : MetaNode { }
    // NU-post <- post-clause
    public class NU_post : MetaNode { }
    // NU-no-SA-handling <- pre-clause NU post-clause
    public class NU_no_SA_handing : MetaNode { }

    // #         change operator to selbri; inverse of MOhE 
    // NUhA-clause <- NUhA-pre NUhA-post
    public class NUhA_clause : MetaNode { }
    // NUhA-pre <- pre-clause NUhA spaces?
    public class NUhA_pre : MetaNode { }
    // NUhA-post <- post-clause
    public class NUhA_post : MetaNode { }
    // NUhA-no-SA-handling <- pre-clause NUhA post-clause
    public class NUhA_no_SA_handing : MetaNode { }

    // #         marks the start of a termset 
    // NUhI-clause <- NUhI-pre NUhI-post
    public class NUhI_clause : MetaNode { }
    // NUhI-pre <- pre-clause NUhI spaces?
    public class NUhI_pre : MetaNode { }
    // NUhI-post <- post-clause
    public class NUhI_post : MetaNode { }
    // NUhI-no-SA-handling <- pre-clause NUhI post-clause
    public class NUhI_no_SA_handing : MetaNode { }

    // #         marks the middle and end of a termset 
    // NUhU-clause <- NUhU-pre NUhU-post
    public class NUhU_clause : MetaNode { }
    // NUhU-pre <- pre-clause NUhU spaces?
    public class NUhU_pre : MetaNode { }
    // NUhU-post <- post-clause
    public class NUhU_post : MetaNode { }
    // NUhU-no-SA-handling <- pre-clause NUhU post-clause
    public class NUhU_no_SA_handing : MetaNode { }


    // #         numbers and numeric punctuation 
    // PA-clause <- PA-pre PA-post
    public class PA_clause : MetaNode { }
    // PA-pre <- pre-clause PA spaces?
    public class PA_pre : MetaNode { }
    // PA-post <- post-clause
    public class PA_post : MetaNode { }
    // PA-no-SA-handling <- pre-clause PA post-clause
    public class PA_no_SA_handing : MetaNode { }

    // #         afterthought termset connective prefix 
    // PEhE-clause <- PEhE-pre PEhE-post
    public class PEhE_clause : MetaNode { }
    // PEhE-pre <- pre-clause PEhE spaces?
    public class PEhE_pre : MetaNode { }
    // PEhE-post <- post-clause
    public class PEhE_post : MetaNode { }
    // PEhE-no-SA-handling <- pre-clause PEhE post-clause
    public class PEhE_no_SA_handing : MetaNode { }

    // #         forethought (Polish) flag 
    // PEhO-clause <- PEhO-pre PEhO-post
    public class PEhO_clause : MetaNode { }
    // PEhO-pre <- pre-clause PEhO spaces?
    public class PEhO_pre : MetaNode { }
    // PEhO-post <- post-clause
    public class PEhO_post : MetaNode { }
    // PEhO-no-SA-handling <- pre-clause PEhO post-clause
    public class PEhO_no_SA_handing : MetaNode { }

    // #         directions in time 
    // PU-clause <- PU-pre PU-post
    public class PU_clause : MetaNode { }
    // PU-pre <- pre-clause PU spaces?
    public class PU_pre : MetaNode { }
    // PU-post <- post-clause
    public class PU_post : MetaNode { }
    // PU-no-SA-handling <- pre-clause PU post-clause
    public class PU_no_SA_handing : MetaNode { }


    // #         flag for modified interpretation of GOhI 
    // RAhO-clause <- RAhO-pre RAhO-post
    public class RAhO_clause : MetaNode { }
    // RAhO-pre <- pre-clause RAhO spaces?
    public class RAhO_pre : MetaNode { }
    // RAhO-post <- post-clause
    public class RAhO_post : MetaNode { }
    // RAhO-no-SA-handling <- pre-clause RAhO post-clause
    public class RAhO_no_SA_handing : MetaNode { }

    // #         converts number to extensional tense 
    // ROI-clause <- ROI-pre ROI-post
    public class ROI_clause : MetaNode { }
    // ROI-pre <- pre-clause ROI spaces?
    public class ROI_pre : MetaNode { }
    // ROI-post <- post-clause
    public class ROI_post : MetaNode { }
    // ROI-no-SA-handling <- pre-clause ROI post-clause
    public class ROI_no_SA_handing : MetaNode { }

    // SA-clause <- SA-pre SA-post
    public class SA_clause : MetaNode { }
    // SA-pre <- pre-clause SA spaces?
    public class SA_pre : MetaNode { }
    // SA-post <- spaces?
    public class SA_post : MetaNode { }

    // #         metalinguistic eraser to the beginning of

    // #                                    the current utterance 

    // #         conversions 
    // SE-clause <- SE-pre SE-post
    public class SE_clause : MetaNode { }
    // SE-pre <- pre-clause SE spaces?
    public class SE_pre : MetaNode { }
    // SE-post <- post-clause
    public class SE_post : MetaNode { }
    // SE-no-SA-handling <- pre-clause SE post-clause
    public class SE_no_SA_handing : MetaNode { }

    // #         metalinguistic bridi insert marker 
    // SEI-clause <- SEI-pre SEI-post
    public class SEI_clause : MetaNode { }
    // SEI-pre <- pre-clause SEI spaces?
    public class SEI_pre : MetaNode { }
    // SEI-post <- post-clause
    public class SEI_post : MetaNode { }
    // SEI-no-SA-handling <- pre-clause SEI post-clause
    public class SEI_no_SA_handing : MetaNode { }

    // #         metalinguistic bridi end marker 
    // SEhU-clause <- SEhU-pre SEhU-post
    public class SEhU_clause : MetaNode { }
    // SEhU-pre <- pre-clause SEhU spaces?
    public class SEhU_pre : MetaNode { }
    // SEhU-post <- post-clause
    public class SEhU_post : MetaNode { }
    // SEhU-no-SA-handling <- pre-clause SEhU post-clause
    public class SEhU_no_SA_handing : MetaNode { }

    // #         metalinguistic single word eraser 
    // SI-clause <- spaces? SI spaces?

    // #         reciprocal sumti marker 
    // SOI-clause <- SOI-pre SOI-post
    public class SOI_clause : MetaNode { }
    // SOI-pre <- pre-clause SOI spaces?
    public class SOI_pre : MetaNode { }
    // SOI-post <- post-clause
    public class SOI_post : MetaNode { }
    // SOI-no-SA-handling <- pre-clause SOI post-clause
    public class SOI_no_SA_handing : MetaNode { }

    // #         metalinguistic eraser of the entire text 
    // SU-clause <- SU-pre SU-post
    public class SU_clause : MetaNode { }
    // SU-pre <- pre-clause SU spaces?
    public class SU_pre : MetaNode { }
    // SU-post <- post-clause
    public class SU_post : MetaNode { }


    // #         tense interval properties 
    // TAhE-clause <- TAhE-pre TAhE-post
    public class TAhE_clause : MetaNode { }
    // TAhE-pre <- pre-clause TAhE spaces?
    public class TAhE_pre : MetaNode { }
    // TAhE-post <- post-clause
    public class TAhE_post : MetaNode { }
    // TAhE-no-SA-handling <- pre-clause TAhE post-clause
    public class TAhE_no_SA_handing : MetaNode { }

    // #         closing gap for MEX constructs 
    // TEhU-clause <- TEhU-pre TEhU-post
    public class TEhU_clause : MetaNode { }
    // TEhU-pre <- pre-clause TEhU spaces?
    public class TEhU_pre : MetaNode { }
    // TEhU-post <- post-clause
    public class TEhU_post : MetaNode { }
    // TEhU-no-SA-handling <- pre-clause TEhU post-clause
    public class TEhU_no_SA_handing : MetaNode { }

    // #         start compound lerfu 
    // TEI-clause <- TEI-pre TEI-post
    public class TEI_clause : MetaNode { }
    // TEI-pre <- pre-clause TEI spaces?
    public class TEI_pre : MetaNode { }
    // TEI-post <- post-clause
    public class TEI_post : MetaNode { }
    // TEI-no-SA-handling <- pre-clause TEI post-clause
    public class TEI_no_SA_handing : MetaNode { }

    // #         left discursive parenthesis 
    // TO-clause <- TO-pre TO-post
    public class TO_clause : MetaNode { }
    // TO-pre <- pre-clause TO spaces?
    public class TO_pre : MetaNode { }
    // TO-post <- post-clause
    public class TO_post : MetaNode { }
    // TO-no-SA-handling <- pre-clause TO post-clause
    public class TO_no_SA_handing : MetaNode { }

    // #         right discursive parenthesis 
    // TOI-clause <- TOI-pre TOI-post
    public class TOI_clause : MetaNode { }
    // TOI-pre <- pre-clause TOI spaces?
    public class TOI_pre : MetaNode { }
    // TOI-post <- post-clause
    public class TOI_post : MetaNode { }
    // TOI-no-SA-handling <- pre-clause TOI post-clause
    public class TOI_no_SA_handing : MetaNode { }

    // #         multiple utterance scope mark 
    // TUhE-clause <- TUhE-pre TUhE-post
    public class TUhE_clause : MetaNode { }
    // TUhE-pre <- pre-clause TUhE spaces?
    public class TUhE_pre : MetaNode { }
    // TUhE-post <- su-clause* post-clause
    // TUhE-no-SA-handling <- pre-clause TUhE su-clause* post-clause
    public class TUhE_no_SA_handing : MetaNode { }

    // #         multiple utterance end scope mark 
    // TUhU-clause <- TUhU-pre TUhU-post
    public class TUhU_clause : MetaNode { }
    // TUhU-pre <- pre-clause TUhU spaces?
    public class TUhU_pre : MetaNode { }
    // TUhU-post <- post-clause
    public class TUhU_post : MetaNode { }
    // TUhU-no-SA-handling <- pre-clause TUhU post-clause
    public class TUhU_no_SA_handing : MetaNode { }


    // #         attitudinals, observationals, discursives 
    // UI-clause <- UI-pre UI-post
    public class UI_clause : MetaNode { }
    // UI-pre <- pre-clause UI spaces?
    public class UI_pre : MetaNode { }
    // UI-post <- post-clause
    public class UI_post : MetaNode { }
    // UI-no-SA-handling <- pre-clause UI post-clause
    public class UI_no_SA_handing : MetaNode { }


    // #         distance in space-time 
    // VA-clause <- VA-pre VA-post
    public class VA_clause : MetaNode { }
    // VA-pre <- pre-clause VA spaces?
    public class VA_pre : MetaNode { }
    // VA-post <- post-clause
    public class VA_post : MetaNode { }
    // VA-no-SA-handling <- pre-clause VA post-clause
    public class VA_no_SA_handing : MetaNode { }

    // #         end simple bridi or bridi-tail 
    // VAU-clause <- VAU-pre VAU-post
    public class VAU_clause : MetaNode { }
    // VAU-pre <- pre-clause VAU spaces?
    public class VAU_pre : MetaNode { }
    // VAU-post <- post-clause
    public class VAU_post : MetaNode { }
    // VAU-no-SA-handling <- pre-clause VAU post-clause
    public class VAU_no_SA_handing : MetaNode { }

    // #         left MEX bracket 
    // VEI-clause <- VEI-pre VEI-post
    public class VEI_clause : MetaNode { }
    // VEI-pre <- pre-clause VEI spaces?
    public class VEI_pre : MetaNode { }
    // VEI-post <- post-clause
    public class VEI_post : MetaNode { }
    // VEI-no-SA-handling <- pre-clause VEI post-clause
    public class VEI_no_SA_handing : MetaNode { }

    // #         right MEX bracket 
    // VEhO-clause <- VEhO-pre VEhO-post
    public class VEhO_clause : MetaNode { }
    // VEhO-pre <- pre-clause VEhO spaces?
    public class VEhO_pre : MetaNode { }
    // VEhO-post <- post-clause
    public class VEhO_post : MetaNode { }
    // VEhO-no-SA-handling <- pre-clause VEhO post-clause
    public class VEhO_no_SA_handing : MetaNode { }

    // #         MEX operator 
    // VUhU-clause <- VUhU-pre VUhU-post
    public class VUhU_clause : MetaNode { }
    // VUhU-pre <- pre-clause VUhU spaces?
    public class VUhU_pre : MetaNode { }
    // VUhU-post <- post-clause
    public class VUhU_post : MetaNode { }
    // VUhU-no-SA-handling <- pre-clause VUhU post-clause
    public class VUhU_no_SA_handing : MetaNode { }

    // #         space-time interval size 
    // VEhA-clause <- VEhA-pre VEhA-post
    public class VEhA_clause : MetaNode { }
    // VEhA-pre <- pre-clause VEhA spaces?
    public class VEhA_pre : MetaNode { }
    // VEhA-post <- post-clause
    public class VEhA_post : MetaNode { }
    // VEhA-no-SA-handling <- pre-clause VEhA post-clause
    public class VEhA_no_SA_handing : MetaNode { }

    // #         space-time dimensionality marker 
    // VIhA-clause <- VIhA-pre VIhA-post
    public class VIhA_clause : MetaNode { }
    // VIhA-pre <- pre-clause VIhA spaces?
    public class VIhA_pre : MetaNode { }
    // VIhA-post <- post-clause
    public class VIhA_post : MetaNode { }
    // VIhA-no-SA-handling <- pre-clause VIhA post-clause
    public class VIhA_no_SA_handling : MetaNode { }
    // VUhO-clause <- VUhO-pre VUhO-post
    public class VUhO_clause : MetaNode { }
    // VUhO-pre <- pre-clause VUhO spaces?
    public class VUhO_pre : MetaNode { }
    // VUhO-post <- post-clause
    public class VUhO_post : MetaNode { }
    // VUhO-no-SA-handling <- pre-clause VUhO post-clause
    public class VUhO_no_SA_handing : MetaNode { }

    // # glue between logically connected sumti and relative clauses 


    // #         subscripting operator 
    // XI-clause <- XI-pre XI-post
    public class XI_clause : MetaNode { }
    // XI-pre <- pre-clause XI spaces?
    public class XI_pre : MetaNode { }
    // XI-post <- post-clause
    public class XI_post : MetaNode { }
    // XI-no-SA-handling <- pre-clause XI post-clause
    public class XI_no_SA_handing : MetaNode { }


    // #         hesitation 
    // # Very very special case.  Handled in the morphology section.
    // # Y-clause <- spaces? Y spaces?
    public class Y_clause : MetaNode { }


    // #         event properties - inchoative, etc. 
    // ZAhO-clause <- ZAhO-pre ZAhO-post
    public class ZAhO_clause : MetaNode { }
    // ZAhO-pre <- pre-clause ZAhO spaces?
    public class ZAhO_pre : MetaNode { }
    // ZAhO-post <- post-clause
    public class ZAhO_post : MetaNode { }
    // ZAhO-no-SA-handling <- pre-clause ZAhO post-clause
    public class ZAhO_no_SA_handing : MetaNode { }

    // #         time interval size tense 
    // ZEhA-clause <- ZEhA-pre ZEhA-post
    public class ZEhA_clause : MetaNode { }
    // ZEhA-pre <- pre-clause ZEhA spaces?
    public class ZEhA_pre : MetaNode { }
    // ZEhA-post <- post-clause
    public class ZEhA_post : MetaNode { }
    // ZEhA-no-SA-handling <- pre-clause ZEhA post-clause
    public class ZEhA_no_SA_handing : MetaNode { }

    // #         lujvo glue 
    // ZEI-clause <- ZEI-pre ZEI-post
    public class ZEI_clause : MetaNode { }
    // ZEI-clause-no-SA <- ZEI-pre-no-SA ZEI ZEI-post
    public class ZEI_clause_no_SA : MetaNode { }
    // ZEI-pre <- pre-clause ZEI spaces?
    public class ZEI_pre : MetaNode { }
    // ZEI-pre-no-SA <- pre-clause
    public class ZEI_pre_no_SA : MetaNode { }
    // ZEI-post <- spaces?
    public class ZEI_post : MetaNode { }
    // ZEI-no-SA-handling <- pre-clause ZEI post-clause
    public class ZEI_no_SA_handing : MetaNode { }

    // #         time distance tense 
    // ZI-clause <- ZI-pre ZI-post
    public class ZI_clause : MetaNode { }
    // ZI-pre <- pre-clause ZI spaces?
    public class ZI_pre : MetaNode { }
    // ZI-post <- post-clause
    public class ZI_post : MetaNode { }
    // ZI-no-SA-handling <- pre-clause ZI post-clause
    public class ZI_no_SA_handing : MetaNode { }

    // #         conjoins relative clauses 
    // ZIhE-clause <- ZIhE-pre ZIhE-post
    public class ZIhE_clause : MetaNode { }
    // ZIhE-pre <- pre-clause ZIhE spaces?
    public class ZIhE_pre : MetaNode { }
    // ZIhE-post <- post-clause
    public class ZIhE_post : MetaNode { }
    // ZIhE-no-SA-handling <- pre-clause ZIhE post-clause
    public class ZIhE_no_SA_handing : MetaNode { }

    // #         single word metalinguistic quote marker 
    // ZO-clause <- ZO-pre ZO-post
    public class ZO_clause : MetaNode { }
    // ZO-pre <- pre-clause ZO spaces? any-word spaces?
    public class ZO_pre : MetaNode { }
    // ZO-post <- post-clause
    public class ZO_post : MetaNode { }
    // ZO-no-SA-handling <- pre-clause ZO spaces? any-word spaces?
    public class ZO_no_SA_handing : MetaNode { }

    // #         delimited quote marker 
    // ZOI-clause <- ZOI-pre ZOI-post
    public class ZOI_clause : MetaNode { }
    // ZOI-pre <- pre-clause ZOI spaces? zoi-open zoi-word* zoi-close spaces?
    public class ZOI_pre : MetaNode { }
    // ZOI-post <- post-clause
    public class ZOI_post : MetaNode { }
    // ZOI-no-SA-handling <- pre-clause ZOI spaces? zoi-open zoi-word* zoi-close spaces?
    public class ZOI_no_SA_handing : MetaNode { }

    // #         prenex terminator (not elidable) 
    // ZOhU-clause <- ZOhU-pre ZOhU-post
    public class ZOhU_clause : MetaNode { }
    // ZOhU-pre <- pre-clause ZOhU spaces?
    public class ZOhU_pre : MetaNode { }
    // ZOhU-post <- post-clause
    public class ZOhU_post : MetaNode { }
    // ZOhU-no-SA-handling <- pre-clause ZOhU post-clause
    public class ZOhU_no_SA_handing : MetaNode { }


    // # --- MORPHOLOGY ---

    // CMENE <- cmene
    // BRIVLA <- gismu / lujvo / fuhivla
    // CMAVO <- A / BAI / BAhE / BE / BEI / BEhO / BIhE / BIhI / BO / BOI / BU / BY / CAhA / CAI / CEI / CEhE / CO / COI / CU / CUhE / DAhO / DOI / DOhU / FA / FAhA / FAhO / FEhE / FEhU / FIhO / FOI / FUhA / FUhE / FUhO / GA / GAhO / GEhU / GI / GIhA / GOI / GOhA / GUhA / I / JA / JAI / JOhI / JOI / KE / KEhE / KEI / KI / KOhA / KU / KUhE / KUhO / LA / LAU / LAhE / LE / LEhU / LI / LIhU / LOhO / LOhU / LU / LUhU / MAhO / MAI / ME / MEhU / MOhE / MOhI / MOI / NA / NAI / NAhE / NAhU / NIhE / NIhO / NOI / NU / NUhA / NUhI / NUhU / PA / PEhE / PEhO / PU / RAhO / ROI / SA / SE / SEI / SEhU / SI / SOI / SU / TAhE / TEhU / TEI / TO / TOI / TUhE / TUhU / UI / VA / VAU / VEI / VEhO / VUhU / VEhA / VIhA / VUhO / XI / ZAhO / ZEhA / ZEI / ZI / ZIhE / ZO / ZOI / ZOhU / cmavo


    // # This is a Parsing Expression Grammar for the morphology of Lojban.
    // # See http://www.pdos.lcs.mit.edu/~baford/packrat/
    // #
    // # All rules have the form
    // #
    // # name <- peg-expression
    // #
    // # which means that the grammatical construct "name" is parsed using
    // # "peg-expression".
    // #
    // # 1) Concatenation is expressed by juxtaposition with no operator symbol.
    // # 2) / represents *ORDERED* alternation (choice). If the first
    // # option succeeds, the others will never be checked.
    // # 3) ? indicates that the element to the left is optional.
    // # 4) * represents optional repetition of the construct to the left.
    // # 5) + represents one-or-more repetition of the construct to the left.
    // # 6) () serves to indicate the grouping of the other operators.
    // # 7) & indicates that the element to the right must follow (but the
    // # marked element itself does not absorb anything).
    // # 8) ! indicates that the element to the right must not follow (the
    // # marked element itself does not absorb anything).
    // # 9) . represents any character.
    // # 10) ' ' or " " represents a literal string.
    // # 11) [] represents a character class.
    // #
    // # Repetitions grab as much as they can.
    // #
    // #
    // # --- GRAMMAR ---
    // # This grammar classifies words by their morphological class (cmene,
    // # gismu, lujvo, fuhivla, cmavo, and non-lojban-word).
    // #
    // #The final section sorts cmavo into grammatical classes (A, BAI, BAhE, ..., ZOhU).
    // #
    // # mi'e ((xorxes))

    // #-------------------------------------------------------------------

    // words <- pause? (word pause?)*
    public class Words : MetaNode { }

    // word <- lojban-word / non-lojban-word
    public class Word : MetaNode { }

    // lojban-word <- cmene / cmavo / brivla
    public class Lojban_word : MetaNode { }

    // #-------------------------------------------------------------------

    // cmene <- !h &consonant-final coda? (any-syllable / digit)* &pause
    public class Cmene : MetaNode { }

    // consonant-final <- (non-space &non-space)* consonant &pause
    public class Consonant_final : MetaNode { }

    // #cmene <- !h cmene-syllable* &consonant coda? consonantal-syllable* onset &pause

    // #cmene-syllable <- !doi-la-lai-lahi coda? consonantal-syllable* onset nucleus / digit

    // #doi-la-lai-lahi <- (d o i / l a (h? i)?) !h !nucleus

    // #-------------------------------------------------------------------

    // cmavo <- !cmene !CVCy-lujvo cmavo-form &post-word
    public class Cmavo : MetaNode { }

    // CVCy-lujvo <- CVC-rafsi y h? initial-rafsi* brivla-core / stressed-CVC-rafsi y short-final-rafsi
    public class CVCy_lujvo : MetaNode { }

    // cmavo-form <- !h !cluster onset (nucleus h)* (!stressed nucleus / nucleus !cluster) / y+ / digit
    public class Cmavo_form : MetaNode { }

    // #-------------------------------------------------------------------

    // brivla <- !cmavo initial-rafsi* brivla-core
    public class Brivla : MetaNode { }

    // brivla-core <- fuhivla / gismu / CVV-final-rafsi / stressed-initial-rafsi short-final-rafsi
    public class Brivla_core : MetaNode { }

    // stressed-initial-rafsi <- stressed-extended-rafsi / stressed-y-rafsi / stressed-y-less-rafsi
    public class Stressed_initial_rafsi : MetaNode { }

    // initial-rafsi <- extended-rafsi / y-rafsi / !any-extended-rafsi y-less-rafsi
    public class Initial_rafsi : MetaNode { }

    // #-------------------------------------------------------------------

    // any-extended-rafsi <- fuhivla / extended-rafsi / stressed-extended-rafsi
    public class Any_extended_rafsi : MetaNode { }

    // fuhivla <- fuhivla-head stressed-syllable consonantal-syllable* final-syllable
    public class Fuhivla : MetaNode { }

    // stressed-extended-rafsi <- stressed-brivla-rafsi / stressed-fuhivla-rafsi
    public class Stressed_extended_rafsi : MetaNode { }

    // extended-rafsi <- brivla-rafsi / fuhivla-rafsi
    public class Extended_rafsi : MetaNode { }

    // stressed-brivla-rafsi <- &unstressed-syllable brivla-head stressed-syllable h y
    public class Stressed_brivla_rafsi : MetaNode { }

    // brivla-rafsi <- &(syllable consonantal-syllable* syllable) brivla-head h y h?
    public class Brivla_rafsi : MetaNode { }
    
    // stressed-fuhivla-rafsi <- fuhivla-head stressed-syllable &consonant onset y
    public class Stressed_fuhivla_rafsi : MetaNode { }

    // fuhivla-rafsi <- &unstressed-syllable fuhivla-head &consonant onset y h?
    public class Fuhivla_rafsi : MetaNode { }

    // fuhivla-head <- !rafsi-string brivla-head
    public class Fuhivla_head : MetaNode { }

    // brivla-head <- !cmavo !slinkuhi !h &onset unstressed-syllable*
    public class Brivla_head : MetaNode { }

    // slinkuhi <- consonant rafsi-string
    public class Slinkuhi_head : MetaNode { }

    // rafsi-string <- y-less-rafsi* (gismu / CVV-final-rafsi / stressed-y-less-rafsi short-final-rafsi / y-rafsi / stressed-y-rafsi / stressed-y-less-rafsi? initial-pair y)
    public class Rafsi_head : MetaNode { }

    // #-------------------------------------------------------------------

    // gismu <- stressed-long-rafsi &final-syllable vowel &post-word
    public class Gismu : MetaNode { }

    // CVV-final-rafsi <- consonant stressed-vowel h &final-syllable vowel &post-word
    public class CVV_final_rafsi : MetaNode { }

    // short-final-rafsi <- &final-syllable (consonant diphthong / initial-pair vowel) &post-word
    public class Short_final_rafsi : MetaNode { }

    // stressed-y-rafsi <- (stressed-long-rafsi / stressed-CVC-rafsi) y
    public class Stressed_y_rafsi : MetaNode { }

    // stressed-y-less-rafsi <- stressed-CVC-rafsi !y / stressed-CCV-rafsi / stressed-CVV-rafsi
    public class Stressed_y_less_rafsi : MetaNode { }

    // stressed-long-rafsi <- (stressed-CCV-rafsi / stressed-CVC-rafsi) consonant
    public class Stressed_long_rafsi : MetaNode { }

    // stressed-CVC-rafsi <- consonant stressed-vowel consonant
    public class Stressed_CVC_less_rafsi : MetaNode { }

    // stressed-CCV-rafsi <- initial-pair stressed-vowel
    public class Stressed_CCV_rafsi : MetaNode { }

    // stressed-CVV-rafsi <- consonant (unstressed-vowel h stressed-vowel / stressed-diphthong) r-hyphen?
    public class Stressed_CVV_rafsi : MetaNode { }

    // y-rafsi <- (long-rafsi / CVC-rafsi) y h?
    public class Y_rafsi : MetaNode { }

    // y-less-rafsi <- !y-rafsi (CVC-rafsi !y / CCV-rafsi / CVV-rafsi) !any-extended-rafsi
    public class Y_less_rafsi : MetaNode { }

    // long-rafsi <- (CCV-rafsi / CVC-rafsi) consonant
    public class Long_rafsi : MetaNode { }

    // CVC-rafsi <- consonant unstressed-vowel consonant
    public class CVC_rafsi : MetaNode { }

    // CCV-rafsi <- initial-pair unstressed-vowel
    public class CCV_rafsi : MetaNode { }

    // CVV-rafsi <- consonant (unstressed-vowel h unstressed-vowel / unstressed-diphthong) r-hyphen?
    public class CVV_rafsi : MetaNode { }

    // r-hyphen <- r &consonant / n &r
    public class R_hyphen : MetaNode { }

    // #-------------------------------------------------------------------

    // final-syllable <- onset !y !stressed nucleus !cmene &post-word
    public class Final_syllable : MetaNode { }

    // stressed-syllable <- &stressed syllable / syllable &stress
    public class Stressed_syllable : MetaNode { }

    // stressed-diphthong <- &stressed diphthong / diphthong &stress
    public class Stressed_diphthong : MetaNode { }

    // stressed-vowel <- &stressed vowel / vowel &stress
    public class Stressed_vowel : MetaNode { }

    // unstressed-syllable <- !stressed syllable !stress / consonantal-syllable
    public class Unstressed_syllable : MetaNode { }

    // unstressed-diphthong <- !stressed diphthong !stress
    public class Unstressed_diphthong : MetaNode { }

    // unstressed-vowel <- !stressed vowel !stress
    public class Unstressed_vowel : MetaNode { }

    // stress <- consonant* y? syllable pause
    public class Stress : MetaNode { }

    // stressed <- onset comma* [AEIOU]
    public class Stressed : MetaNode { }

    // any-syllable <- onset nucleus coda? / consonantal-syllable
    public class Any_syllable : MetaNode { }

    // syllable <- onset !y nucleus coda?
    public class Syllable : MetaNode { }

    // consonantal-syllable <- consonant syllabic &(consonantal-syllable / onset) (consonant &spaces)?
    public class Consonantal_syllable : MetaNode { }

    // coda <- !any-syllable consonant &any-syllable / syllabic? consonant? &pause
    public class Coda : MetaNode { }

    // onset <- h / consonant? glide / initial
    public class Onset : MetaNode { }

    // nucleus <- vowel / diphthong / y !nucleus
    public class Nucleus : MetaNode { }

    // #-----------------------------------------------------------------

    // glide <- (i / u) &nucleus !glide
    public class Glide : MetaNode { }

    // diphthong <- (a i / a u / e i / o i) !nucleus !glide
    public class Diphthong : MetaNode { }

    // vowel <- (a / e / i / o / u) !nucleus
    public class Vowel : MetaNode { }

    // a <- comma* [aA]
    public class _a : MetaNode { }

    // e <- comma* [eE]
    public class _e : MetaNode { }

    // i <- comma* [iI]
    public class _i : MetaNode { }

    // o <- comma* [oO]
    public class _o : MetaNode { }

    // u <- comma* [uU]
    public class _u : MetaNode { }

    // y <- comma* [yY]
    public class _y : MetaNode { }

    // #-------------------------------------------------------------------

    // cluster <- consonant consonant+
    public class Cluster : MetaNode { }

    // initial-pair <- &initial consonant consonant !consonant
    public class Initial_pair : MetaNode { }

    // initial <- (affricate / sibilant? other? liquid?) !consonant !glide
    public class Initial : MetaNode { }

    // affricate <- t c / t s / d j / d z
    public class Affricate : MetaNode { }

    // liquid <- l / r
    public class Liquid : MetaNode { }

    // other <- p / t !l / k / f / x / b / d !l / g / v / m / n !liquid
    public class Other : MetaNode { }

    // sibilant <- c / s !x / (j / z) !n !liquid
    public class Sibilant : MetaNode { }

    // consonant <- voiced / unvoiced / syllabic
    public class Consonant : MetaNode { }

    // syllabic <- l / m / n / r
    public class Syllabic : MetaNode { }

    // voiced <- b / d / g / j / v / z
    public class Voiced : MetaNode { }

    // unvoiced <- c / f / k / p / s / t / x
    public class Unvoiced : MetaNode { }

    // l <- comma* [lL] !h !l
    public class _y : MetaNode { }

    // m <- comma* [mM] !h !m !z
    public class _y : MetaNode { }

    // n <- comma* [nN] !h !n !affricate
    public class _y : MetaNode { }

    // r <- comma* [rR] !h !r
    public class _y : MetaNode { }

    // b <- comma* [bB] !h !b !unvoiced
    public class _y : MetaNode { }

    // d <- comma* [dD] !h !d !unvoiced
    public class _y : MetaNode { }

    // g <- comma* [gG] !h !g !unvoiced
    public class _y : MetaNode { }

    // v <- comma* [vV] !h !v !unvoiced
    public class _y : MetaNode { }

    // j <- comma* [jJ] !h !j !z !unvoiced
    public class _y : MetaNode { }

    // z <- comma* [zZ] !h !z !j !unvoiced
    public class _y : MetaNode { }

    // s <- comma* [sS] !h !s !c !voiced
    public class _y : MetaNode { }

    // c <- comma* [cC] !h !c !s !x !voiced
    public class _y : MetaNode { }

    // x <- comma* [xX] !h !x !c !k !voiced
    public class _y : MetaNode { }

    // k <- comma* [kK] !h !k !x !voiced
    public class _y : MetaNode { }

    // f <- comma* [fF] !h !f !voiced
    public class _y : MetaNode { }

    // p <- comma* [pP] !h !p !voiced
    public class _y : MetaNode { }

    // t <- comma* [tT] !h !t !voiced
    public class _y : MetaNode { }

    // h <- comma* ['h] &nucleus
    public class _y : MetaNode { }

    // #-------------------------------------------------------------------

    // digit <- comma* [0123456789] !h !nucleus
    public class _y : MetaNode { }

    // post-word <- pause / !nucleus lojban-word
    public class _y : MetaNode { }

    // pause <- comma* space-char / EOF
    public class _y : MetaNode { }

    // EOF <- comma* !.
    public class _y : MetaNode { }

    // comma <- [,]
    public class _y : MetaNode { }

    // non-lojban-word <- !lojban-word non-space+
    public class _y : MetaNode { }

    // non-space <- !space-char .
    public class _y : MetaNode { }

    // #Unicode-style and escaped chars not compatible with cl-peg
    // # space-char <- [.\t\n\r?!\u0020]

    // space-char <- [.?! ] / space-char1 / space-char2 
    public class _y : MetaNode { }
    // space-char1 <- '	'
    public class _y : MetaNode { }
    // space-char2 <- '
    // '
    public class _y : MetaNode { }

    // #-------------------------------------------------------------------

    // spaces <- !Y initial-spaces
    public class _y : MetaNode { }

    // initial-spaces <- (comma* space-char / !ybu Y)+ EOF? / EOF
    public class _y : MetaNode { }

    // ybu <- Y space-char* BU
    public class _y : MetaNode { }

    // lujvo <- !gismu !fuhivla brivla
    public class _y : MetaNode { }

    // #-------------------------------------------------------------------

    // A <- &cmavo ( a / e / j i / o / u ) &post-word
    public class A : MetaNode { }

    // BAI <- &cmavo ( d u h o / s i h u / z a u / k i h i / d u h i / c u h u / t u h i / t i h u / d i h o / j i h u / r i h a / n i h i / m u h i / k i h u / v a h u / k o i / c a h i / t a h i / p u h e / j a h i / k a i / b a i / f i h e / d e h i / c i h o / m a u / m u h u / r i h i / r a h i / k a h a / p a h u / p a h a / l e h a / k u h u / t a i / b a u / m a h i / c i h e / f a u / p o h i / c a u / m a h e / c i h u / r a h a / p u h a / l i h e / l a h u / b a h i / k a h i / s a u / f a h e / b e h i / t i h i / j a h e / g a h a / v a h o / j i h o / m e h a / d o h e / j i h e / p i h o / g a u / z u h e / m e h e / r a i ) &post-word
    public class A : MetaNode { }

    // BAhE <- &cmavo ( b a h e / z a h e ) &post-word
    public class A : MetaNode { }

    // BE <- &cmavo ( b e ) &post-word
    public class A : MetaNode { }

    // BEI <- &cmavo ( b e i ) &post-word
    public class A : MetaNode { }

    // BEhO <- &cmavo ( b e h o ) &post-word
    public class A : MetaNode { }

    // BIhE <- &cmavo ( b i h e ) &post-word
    public class A : MetaNode { }

    // BIhI <- &cmavo ( m i h i / b i h o / b i h i ) &post-word
    public class A : MetaNode { }

    // BO <- &cmavo ( b o ) &post-word
    public class A : MetaNode { }

    // BOI <- &cmavo ( b o i ) &post-word
    public class A : MetaNode { }

    // BU <- &cmavo ( b u ) &post-word
    public class A : MetaNode { }

    // BY <- ybu / &cmavo ( j o h o / r u h o / g e h o / j e h o / l o h a / n a h a / s e h e / t o h a / g a h e / y h y / b y / c y / d y / f y / g y / j y / k y / l y / m y / n y / p y / r y / s y / t y / v y / x y / z y ) &post-word
    public class A : MetaNode { }

    // CAhA <- &cmavo ( c a h a / p u h i / n u h o / k a h e ) &post-word
    public class A : MetaNode { }

    // CAI <- &cmavo ( p e i / c a i / c u h i / s a i / r u h e ) &post-word
    public class A : MetaNode { }

    // CEI <- &cmavo ( c e i ) &post-word
    public class A : MetaNode { }

    // CEhE <- &cmavo ( c e h e ) &post-word
    public class A : MetaNode { }

    // CO <- &cmavo ( c o ) &post-word
    public class A : MetaNode { }

    // COI <- &cmavo ( j u h i / c o i / f i h i / t a h a / m u h o / f e h o / c o h o / p e h u / k e h o / n u h e / r e h i / b e h e / j e h e / m i h e / k i h e / v i h o ) &post-word
    public class A : MetaNode { }

    // CU <- &cmavo ( c u ) &post-word
    public class A : MetaNode { }

    // CUhE <- &cmavo ( c u h e / n a u ) &post-word
    public class A : MetaNode { }

    // DAhO <- &cmavo ( d a h o ) &post-word
    public class A : MetaNode { }

    // DOI <- &cmavo ( d o i ) &post-word
    public class A : MetaNode { }

    // DOhU <- &cmavo ( d o h u ) &post-word
    public class A : MetaNode { }

    // FA <- &cmavo ( f a i / f a / f e / f o / f u / f i h a / f i ) &post-word
    public class A : MetaNode { }

    // FAhA <- &cmavo ( d u h a / b e h a / n e h u / v u h a / g a h u / t i h a / n i h a / c a h u / z u h a / r i h u / r u h u / r e h o / t e h e / b u h u / n e h a / p a h o / n e h i / t o h o / z o h i / z e h o / z o h a / f a h a ) &post-word
    public class A : MetaNode { }

    // FAhO <- &cmavo ( f a h o ) &post-word
    public class A : MetaNode { }

    // FEhE <- &cmavo ( f e h e ) &post-word
    public class A : MetaNode { }

    // FEhU <- &cmavo ( f e h u ) &post-word
    public class A : MetaNode { }

    // FIhO <- &cmavo ( f i h o ) &post-word
    public class A : MetaNode { }

    // FOI <- &cmavo ( f o i ) &post-word
    public class A : MetaNode { }

    // FUhA <- &cmavo ( f u h a ) &post-word
    public class A : MetaNode { }

    // FUhE <- &cmavo ( f u h e ) &post-word
    public class A : MetaNode { }

    // FUhO <- &cmavo ( f u h o ) &post-word
    public class A : MetaNode { }

    // GA <- &cmavo ( g e h i / g e / g o / g a / g u ) &post-word
    public class A : MetaNode { }

    // GAhO <- &cmavo ( k e h i / g a h o ) &post-word
    public class A : MetaNode { }

    // GEhU <- &cmavo ( g e h u ) &post-word
    public class A : MetaNode { }

    // GI <- &cmavo ( g i ) &post-word
    public class A : MetaNode { }

    // GIhA <- &cmavo ( g i h e / g i h i / g i h o / g i h a / g i h u ) &post-word
    public class A : MetaNode { }

    // GOI <- &cmavo ( n o h u / n e / g o i / p o h u / p e / p o h e / p o ) &post-word
    public class A : MetaNode { }

    // GOhA <- &cmavo ( m o / n e i / g o h u / g o h o / g o h i / n o h a / g o h e / g o h a / d u / b u h a / b u h e / b u h i / c o h e ) &post-word
    public class A : MetaNode { }

    // GUhA <- &cmavo ( g u h e / g u h i / g u h o / g u h a / g u h u ) &post-word
    public class A : MetaNode { }

    // I <- &cmavo ( i ) &post-word
    public class A : MetaNode { }

    // JA <- &cmavo ( j e h i / j e / j o / j a / j u ) &post-word
    public class A : MetaNode { }

    // JAI <- &cmavo ( j a i ) &post-word
    public class A : MetaNode { }

    // JOhI <- &cmavo ( j o h i ) &post-word
    public class A : MetaNode { }

    // JOI <- &cmavo ( f a h u / p i h u / j o i / c e h o / c e / j o h u / k u h a / j o h e / j u h e ) &post-word
    public class A : MetaNode { }

    // KE <- &cmavo ( k e ) &post-word
    public class A : MetaNode { }

    // KEhE <- &cmavo ( k e h e ) &post-word
    public class A : MetaNode { }

    // KEI <- &cmavo ( k e i ) &post-word
    public class A : MetaNode { }

    // KI <- &cmavo ( k i ) &post-word
    public class A : MetaNode { }

    // KOhA <- &cmavo ( d a h u / d a h e / d i h u / d i h e / d e h u / d e h e / d e i / d o h i / m i h o / m a h a / m i h a / d o h o / k o h a / f o h u / k o h e / k o h i / k o h o / k o h u / f o h a / f o h e / f o h i / f o h o / v o h a / v o h e / v o h i / v o h o / v o h u / r u / r i / r a / t a / t u / t i / z i h o / k e h a / m a / z u h i / z o h e / c e h u / d a / d e / d i / k o / m i / d o ) &post-word
    public class A : MetaNode { }

    // KU <- &cmavo ( k u ) &post-word
    public class A : MetaNode { }

    // KUhE <- &cmavo ( k u h e ) &post-word
    public class A : MetaNode { }

    // KUhO <- &cmavo ( k u h o ) &post-word
    public class A : MetaNode { }

    // LA <- &cmavo ( l a i / l a h i / l a ) &post-word
    public class A : MetaNode { }

    // LAU <- &cmavo ( c e h a / l a u / z a i / t a u ) &post-word
    public class A : MetaNode { }

    // LAhE <- &cmavo ( t u h a / l u h a / l u h o / l a h e / v u h i / l u h i / l u h e ) &post-word
    public class A : MetaNode { }

    // LE <- &cmavo ( l e i / l o i / l e h i / l o h i / l e h e / l o h e / l o / l e ) &post-word
    public class A : MetaNode { }

    // LEhU <- &cmavo ( l e h u ) &post-word
    public class A : MetaNode { }

    // LI <- &cmavo ( m e h o / l i ) &post-word
    public class A : MetaNode { }

    // LIhU <- &cmavo ( l i h u ) &post-word
    public class A : MetaNode { }

    // LOhO <- &cmavo ( l o h o ) &post-word
    public class A : MetaNode { }

    // LOhU <- &cmavo ( l o h u ) &post-word
    public class A : MetaNode { }

    // LU <- &cmavo ( l u ) &post-word
    public class A : MetaNode { }

    // LUhU <- &cmavo ( l u h u ) &post-word
    public class A : MetaNode { }

    // MAhO <- &cmavo ( m a h o ) &post-word
    public class A : MetaNode { }

    // MAI <- &cmavo ( m o h o / m a i ) &post-word
    public class A : MetaNode { }

    // ME <- &cmavo ( m e ) &post-word
    public class A : MetaNode { }

    // MEhU <- &cmavo ( m e h u ) &post-word
    public class A : MetaNode { }

    // MOhE <- &cmavo ( m o h e ) &post-word
    public class A : MetaNode { }

    // MOhI <- &cmavo ( m o h i ) &post-word
    public class A : MetaNode { }

    // MOI <- &cmavo ( m e i / m o i / s i h e / c u h o / v a h e ) &post-word
    public class A : MetaNode { }

    // NA <- &cmavo ( j a h a / n a ) &post-word
    public class A : MetaNode { }

    // NAI <- &cmavo ( n a i ) &post-word
    public class A : MetaNode { }

    // NAhE <- &cmavo ( t o h e / j e h a / n a h e / n o h e ) &post-word
    public class A : MetaNode { }

    // NAhU <- &cmavo ( n a h u ) &post-word
    public class A : MetaNode { }

    // NIhE <- &cmavo ( n i h e ) &post-word
    public class A : MetaNode { }

    // NIhO <- &cmavo ( n i h o / n o h i ) &post-word
    public class A : MetaNode { }

    // NOI <- &cmavo ( v o i / n o i / p o i ) &post-word
    public class A : MetaNode { }

    // NU <- &cmavo ( n i / d u h u / s i h o / n u / l i h i / k a / j e i / s u h u / z u h o / m u h e / p u h u / z a h i ) &post-word
    public class A : MetaNode { }

    // NUhA <- &cmavo ( n u h a ) &post-word
    public class A : MetaNode { }

    // NUhI <- &cmavo ( n u h i ) &post-word
    public class A : MetaNode { }

    // NUhU <- &cmavo ( n u h u ) &post-word 
    public class A : MetaNode { }

    // PA <- &cmavo ( d a u / f e i / g a i / j a u / r e i / v a i / p i h e / p i / f i h u / z a h u / m e h i / n i h u / k i h o / c e h i / m a h u / r a h e / d a h a / s o h a / j i h i / s u h o / s u h e / r o / r a u / s o h u / s o h i / s o h e / s o h o / m o h a / d u h e / t e h o / k a h o / c i h i / t u h o / x o / p a i / n o h o / n o / p a / r e / c i / v o / m u / x a / z e / b i / s o / digit ) &post-word
    public class A : MetaNode { }

    // PEhE <- &cmavo ( p e h e ) &post-word
    public class A : MetaNode { }

    // PEhO <- &cmavo ( p e h o ) &post-word
    public class A : MetaNode { }

    // PU <- &cmavo ( b a / p u / c a ) &post-word
    public class A : MetaNode { }

    // RAhO <- &cmavo ( r a h o ) &post-word
    public class A : MetaNode { }

    // ROI <- &cmavo ( r e h u / r o i ) &post-word
    public class A : MetaNode { }

    // SA <- &cmavo ( s a ) &post-word
    public class A : MetaNode { }

    // SE <- &cmavo ( s e / t e / v e / x e ) &post-word
    public class A : MetaNode { }

    // SEI <- &cmavo ( s e i / t i h o ) &post-word
    public class A : MetaNode { }

    // SEhU <- &cmavo ( s e h u ) &post-word
    public class A : MetaNode { }

    // SI <- &cmavo ( s i ) &post-word
    public class A : MetaNode { }

    // SOI <- &cmavo ( s o i ) &post-word
    public class A : MetaNode { }

    // SU <- &cmavo ( s u ) &post-word
    public class A : MetaNode { }


    // TAhE <- &cmavo ( r u h i / t a h e / d i h i / n a h o ) &post-word
    public class A : MetaNode { }

    // TEhU <- &cmavo ( t e h u ) &post-word
    public class A : MetaNode { }

    // TEI <- &cmavo ( t e i ) &post-word
    public class A : MetaNode { }

    // TO <- &cmavo ( t o h i / t o ) &post-word
    public class A : MetaNode { }

    // TOI <- &cmavo ( t o i ) &post-word
    public class A : MetaNode { }

    // TUhE <- &cmavo ( t u h e ) &post-word
    public class A : MetaNode { }

    // TUhU <- &cmavo ( t u h u ) &post-word
    public class A : MetaNode { }

    // UI <- &cmavo ( i h a / i e / a h e / u h i / i h o / i h e / a h a / i a / o h i / o h e / e h e / o i / u o / e h i / u h o / a u / u a / a h i / i h u / i i / u h a / u i / a h o / a i / a h u / i u / e i / o h o / e h a / u u / o h a / o h u / u h u / e h o / i o / e h u / u e / i h i / u h e / b a h a / j a h o / c a h e / s u h a / t i h e / k a h u / s e h o / z a h a / p e h i / r u h a / j u h a / t a h o / r a h u / l i h a / b a h u / m u h a / d o h a / t o h u / v a h i / p a h e / z u h u / s a h e / l a h a / k e h u / s a h u / d a h i / j e h u / s a h a / k a u / t a h u / n a h i / j o h a / b i h u / l i h o / p a u / m i h u / k u h i / j i h a / s i h a / p o h o / p e h a / r o h i / r o h e / r o h o / r o h u / r o h a / r e h e / l e h o / j u h o / f u h i / d a i / g a h i / z o h o / b e h u / r i h e / s e h i / s e h a / v u h e / k i h a / x u / g e h e / b u h o ) &post-word
    public class A : MetaNode { }

    // VA <- &cmavo ( v i / v a / v u ) &post-word
    public class A : MetaNode { }

    // VAU <- &cmavo ( v a u ) &post-word
    public class A : MetaNode { }

    // VEI <- &cmavo ( v e i ) &post-word
    public class A : MetaNode { }

    // VEhO <- &cmavo ( v e h o ) &post-word
    public class A : MetaNode { }

    // VUhU <- &cmavo ( g e h a / f u h u / p i h i / f e h i / v u h u / s u h i / j u h u / g e i / p a h i / f a h i / t e h a / c u h a / v a h a / n e h o / d e h o / f e h a / s a h o / r e h a / r i h o / s a h i / p i h a / s i h i ) &post-word
    public class A : MetaNode { }

    // VEhA <- &cmavo ( v e h u / v e h a / v e h i / v e h e ) &post-word
    public class A : MetaNode { }

    // VIhA <- &cmavo ( v i h i / v i h a / v i h u / v i h e ) &post-word
    public class A : MetaNode { }

    // VUhO <- &cmavo ( v u h o ) &post-word
    public class A : MetaNode { }

    // XI <- &cmavo ( x i ) &post-word
    public class A : MetaNode { }

    // Y <- &cmavo ( y+ ) &post-word
    public class A : MetaNode { }

    // ZAhO <- &cmavo ( c o h i / p u h o / c o h u / m o h u / c a h o / c o h a / d e h a / b a h o / d i h a / z a h o ) &post-word
    public class A : MetaNode { }

    // ZEhA <- &cmavo ( z e h u / z e h a / z e h i / z e h e ) &post-word
    public class A : MetaNode { }

    // ZEI <- &cmavo ( z e i ) &post-word
    public class A : MetaNode { }

    // ZI <- &cmavo ( z u / z a / z i ) &post-word
    public class A : MetaNode { }

    // ZIhE <- &cmavo ( z i h e ) &post-word
    public class A : MetaNode { }

    // ZO <- &cmavo ( z o ) &post-word
    public class A : MetaNode { }

    // ZOI <- &cmavo ( z o i / l a h o ) &post-word
    public class A : MetaNode { }

    // ZOhU <- &cmavo ( z o h u ) &post-word
    public class A : MetaNode { }


    public class EOF : MetaNode { }
    public class Spaces : MetaNode { }
    public class SI_clause : MetaNode { }

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
